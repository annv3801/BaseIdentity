using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable All
#pragma warning disable 8618

namespace Application.DTO.Account.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdateMyAccountRequest
    {
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? AvatarPhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public bool? Gender { get; set; }
        public List<Guid>? Roles { get; set; }
    }
}