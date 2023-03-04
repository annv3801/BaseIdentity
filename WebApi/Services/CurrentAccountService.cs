using System.Security.Claims;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models.Account;
using Domain.Constants;
using Domain.Exceptions;

namespace WebApi.Services;

public class CurrentAccountService : ICurrentAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentAccountService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid Id
    {
        get
        {
            Guid.TryParse(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.UserId),
                out var userId);
            if (userId == Guid.Empty)
            {
                throw new AccountNotFoundException(LocalizationString.Account.NotLogin);
            }

            return userId;
        }
    }

    public GeneralAccountView Account
    {
        get { return new GeneralAccountView(); }
    }

    public bool HasPerm(string perm)
    {
        return _httpContextAccessor.HttpContext?.User.Claims.Any(claim =>
            claim.Type == JwtClaimTypes.Permission && claim.Value == perm) ?? false;
    }

    public string GetJwtToken()
    {
        var result =
            _httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(h => h.Key == "Authorization");
        return result?.Value.ToString().Replace("Bearer ", "");
    }

    public bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.Any(claim =>
            claim.Type == JwtClaimTypes.Permission && claim.Value == Constants.Permissions.SysAdmin) ?? false;
    }
}
