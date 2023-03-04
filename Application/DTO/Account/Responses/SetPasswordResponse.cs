using System.Diagnostics.CodeAnalysis;
// ReSharper disable All
#pragma warning disable 8618

namespace Application.DTO.Account.Responses;
[ExcludeFromCodeCoverage]
public class SetPasswordResponse
{
    public string GeneratedPassword { get; set; }
}
