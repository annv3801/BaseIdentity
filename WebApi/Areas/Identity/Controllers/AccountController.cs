using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common;
using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Queries;
using Application.Identity.Account.Services;
using AutoMapper;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Infrastructure.Common.Attributes.ActionLog;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Areas.Identity.Controllers;
[ApiController]
[Area(Common.Url.Areas.Identity)]
[SwaggerTag(Constants.SwaggerTags.Account)]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IAccountManagementService _accountManagementService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IJwtService _jwtService;

    public AccountController(IMediator mediator, IMapper mapper, ILoggerService loggerService, ICurrentAccountService currentAccountService, IStringLocalizationService localizationService, IAccountManagementService accountManagementService, IApplicationDbContext applicationDbContext, IJwtService jwtService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
        _currentAccountService = currentAccountService;
        _localizationService = localizationService;
        _accountManagementService = accountManagementService;
        _applicationDbContext = applicationDbContext;
        _jwtService = jwtService;
    }

    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.Identity.Account.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> CreateAccountAsync(CreateAccountRequest createAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createUserCommand = _mapper.Map<CreateAccountCommand>(createAccountRequest);
            var result = await _mediator.Send(createUserCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateAccountAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    [LogAction]
    // [Permissions]
    [Route(Common.Url.Identity.Account.ViewMytAccount)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewMyAccountAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentUserId = _currentAccountService.Id;
            var result = await _mediator.Send(new ViewAccountQuery()
            {
                AccountId = currentUserId
            }, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewMyAccountAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut]
    // [Permissions]
    [LogAction]
    [Route(Common.Url.Identity.Account.ChangePassword)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<ChangePasswordCommand>(changePasswordRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ChangePasswordAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [LogAction]
    [Route(Common.Url.Identity.Account.SignInWithUserName)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> SignInWithUserNameAsync(SignInWithUserNameRequest signInWithUserNameRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<SignInWithUserNameCommand>(signInWithUserNameRequest);
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(SignInWithUserNameAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    private static ClaimsIdentity BuildClaimsIdentity(Domain.Entities.Identity.Account account)
    {
        var claims = new List<Claim>();
        foreach (var userRole in account.AccountRoles)
        {
            if (userRole.Role == null) continue;
            claims.Add(new Claim(JwtClaimTypes.Role, userRole.Role.Name));

            claims.AddRange(from rolePermission in userRole.Role?.RolePermissions ?? new List<RolePermission>() where rolePermission.Permission != null select new Claim(JwtClaimTypes.Permission, rolePermission.Permission?.Code ?? string.Empty));
        }

        claims.Add(new Claim(JwtClaimTypes.IdentityProvider, Constants.LoginProviders.Self));
        claims.Add(new Claim(JwtClaimTypes.UserId, account.Id.ToString()));

        var claimsIdentity = new ClaimsIdentity(claims);
        return claimsIdentity;
    }
    private  string DecryptText(string cipherText, string encryptionPrivateKey = "") 
    {
        if (String.IsNullOrEmpty(cipherText))
            return cipherText;

    
        var tDESalg = new TripleDESCryptoServiceProvider();
        tDESalg.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
        tDESalg.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

        byte[] buffer = Convert.FromBase64String(cipherText);
        return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
    }
    private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv) 
    {
        using (var ms = new MemoryStream(data)) {
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                using (var sr = new StreamReader(cs, Encoding.Unicode))
                {
                    return sr.ReadLine();
                }
            }
        }
    }

    [HttpDelete]
    [LogAction]
    // [Permissions]
    [Route(Common.Url.Identity.Account.Logout)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> LogoutAsync([FromQuery] bool forceEndOtherSessions, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new LogOutCommand()
            {
                ForceEndOtherSessions = forceEndOtherSessions
            }, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(LogoutAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}
