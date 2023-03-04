using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using MediatR;

namespace Application.Identity.Account.Commands;
[ExcludeFromCodeCoverage]
public class CreateAccountCommand : IRequest<Result<CreateAccountResponse>>
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AvatarPhoto { get; set; }
    public string? CoverPhoto { get; set; }
    public bool? Gender { get; set; }
    public string Password { get; set; }
    public List<Guid>? Roles { get; set; }
}
