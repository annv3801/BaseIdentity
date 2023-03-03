using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.SwaggerUI
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            // Configure Bearer authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "Enter 'Bearer [Access Token]' in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1aAAWcFCI6IkpXVCJ9\""
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        }
    }
}