using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Identity.Account.Repositories;
using Domain.Constants;
using Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Services;
using WebApi.SwaggerUI;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApi
{
    /// <summary>
    /// Inject Dependencies on Web Api
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityService(configuration);
            services.ConfigureServerOptions();
            services.AddWebCoreServices();
            services.ConfigureCorsOptions();
            services.AddLocalizationServices();
            services.AddSwaggerUiService();
            services.AddConfigurations(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // To enable the customized ViewModelValidationFilter
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
        private static void AddLocalizationServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizationService, StringLocalizationServiceService>();
            services.AddLocalization(option => { option.ResourcesPath = "Resources"; });
        }
        private static void AddWebCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentAccountService, CurrentAccountService>();
            services.AddMediatR(typeof(Infrastructure.DependencyInjection));
            services.AddAutoMapper(typeof(Infrastructure.DependencyInjection));
            // For worker to work with Newtonsoft
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = Constants.JsonDateTimeFormat
            };
        }
        private static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationConfiguration>(configuration.GetSection("ApplicationConfigurations"));
            services.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfigurations"));
        } 
        
        private static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // Configure JWT authentication mechanism
            services.AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                //.AddCookie(config => config.SlidingExpiration = true)
                .AddJwtBearer(config =>
                {
                    //cfg.
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["JwtConfigurations:Issuer"],
                        ValidAudience = configuration["JwtConfigurations:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["JwtConfigurations:SymmetricSecurityKey"])),
                    };
                    config.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/notification/web")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context =>
                        {
                            // To check token is valid and must be existing in the UserToken table in the database
                            // Once JWT is not existing in the UserToken table, the authentication process will be set as failed.

                            var userIdClaim =
                                context.Principal?.Claims.FirstOrDefault(claim => claim.Type == JwtClaimTypes.UserId);
                            if (userIdClaim == null)
                            {
                                context.Fail("JWT Token does not contain User Id Claim.");
                            }

                            var accountRepository = context.HttpContext.RequestServices.GetRequiredService<IAccountRepository>();

                            var token = context.HttpContext.Request.Headers["Authorization"].ToString()
                                .Replace("Bearer ", "");
                            // If we cannot get token from header, try to use from querystring (for wss)
                            if (token.IsMissing())
                            {
                                token = context.Request.Query["access_token"];
                            }

                            // Check the token from the db
                            if (!await accountRepository.IsValidJwtAsync(
                                Guid.Parse(userIdClaim?.Value ?? string.Empty), token,
                                Constants.LoginProviders.Self, CancellationToken.None))
                            {
                                context.Fail("JWT Token is invalid or no longer exists.");
                            }

                            Console.WriteLine(@"Token Validated OK");
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            // Ensure we always have an error and error description.
                            if (string.IsNullOrEmpty(context.Error))
                                context.Error = "invalid_token";
                            if (string.IsNullOrEmpty(context.ErrorDescription))
                            {
                                // Pass the message from OnTokenValidated on method context.Fail(<message>)
                                if (context.AuthenticateFailure != null &&
                                    context.AuthenticateFailure.Message.Length > 0)
                                {
                                    context.ErrorDescription = context.AuthenticateFailure.Message;
                                }
                                else
                                {
                                    // If we dont have error message from OnTokenValidated, set a message
                                    context.ErrorDescription =
                                        "This request requires a valid JWT access token to be provided.";
                                }
                            }

                            // Add some extra context for expired tokens.
                            if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() ==
                                typeof(SecurityTokenExpiredException))
                            {
                                var authenticationException =
                                    context.AuthenticateFailure as SecurityTokenExpiredException;
                                context.Response.Headers.Add("WWW-Authenticate", "Bearer");
                                context.ErrorDescription = $"The token expired on {authenticationException?.Expires:o}";
                            }

                            return context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                status = 401,
                                error = context.Error,
                                errorDescription = context.ErrorDescription
                            }));
                        }
                    };
                });
        }


        /// <summary>
        /// Configure Server option
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static void ConfigureServerOptions(this IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
            });
        }

        /// <summary>
        /// Configure CORS
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static void ConfigureCorsOptions(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // options.AddPolicy(name: AllowSpecificOrigins,
                //     builder =>
                //     {
                //         builder.WithOrigins("*");
                //     });
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials(); // allow any host, but should be update as production env
                });
            });
        }
        private static void AddSwaggerUiService(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(c =>
            {
                c.ExampleFilters();
                c.EnableAnnotations();
                c.SchemaFilter<SchemaFilter>();
                // c.DocumentFilter<DocumentOperationFilter>();
                // c.OperationFilter<DocumentOperationFilter>();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Program>();
        }
    }

    /// <summary>
    /// Register localization services
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AppLocalizationServiceExtension
    {
        /// <summary>
        /// Localization services
        /// </summary>
        /// <param name="app"></param>
        public static void UseAppLocalizationService(this IApplicationBuilder app)
        {
            // Configure supported languages
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(Constants.SupportedCultures.English)
                .AddSupportedCultures(Constants.SupportedCultures.Cultures)
                .AddSupportedUICultures(Constants.SupportedCultures.Cultures);
            app.UseRequestLocalization(localizationOptions);
        }
    }
    
}