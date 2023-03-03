using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.Jwt.Requests;
using Application.DTO.Jwt.Responses;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfiguration _jwtOptions;
        private readonly ILoggerService _loggerService;
        private readonly IDateTime _dateTime;

        public JwtService(IOptions<JwtConfiguration> jwtOptions, ILoggerService loggerService, IDateTime dateTime)
        {
            _jwtOptions = jwtOptions.Value;
            _loggerService = loggerService;
            _dateTime = dateTime;
        }

        public async Task<Result<CreateJwtResponse>> GenerateJwtAsync(Account account, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.SymmetricSecurityKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _jwtOptions.Audience,
                    Issuer = _jwtOptions.Issuer,
                    Subject = claimsIdentity,
                    Expires = _dateTime.UtcNow.AddHours(_jwtOptions.Expires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                await Task.CompletedTask;
                return Result<CreateJwtResponse>.Succeed(new CreateJwtResponse()
                {
                    Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                    RefreshToken = GenerateRefreshJwtToken(account, cancellationToken)
                });
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, "Error while generating JWT");
                throw;
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private string GenerateRefreshJwtToken(Account account, CancellationToken cancellationToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.SymmetricSecurityKey);
                var claims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.UserId, account.Id.ToString()),
                    new Claim(JwtClaimTypes.IdentityProvider, Constants.LoginProviders.Self),
                };
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _jwtOptions.Audience,
                    Issuer = _jwtOptions.Issuer,
                    Subject = new ClaimsIdentity(claims),
                    Expires = _dateTime.UtcNow.AddHours(_jwtOptions.RefreshTokenExpires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, "Error while generating JWT Refresh Token");
                throw;
            }
        }

        public Task<Result<CreateJwtResponse>> RenewJwtAsync(RenewJwtRequest renewJwtRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<DeleteJwtResponse>> DeleteJwtAsync(string token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}