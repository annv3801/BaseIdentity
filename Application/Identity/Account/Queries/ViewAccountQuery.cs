using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using MediatR;

namespace Application.Identity.Account.Queries
{
    /// <summary>
    /// UserDetailDto: Return data type
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ViewAccountQuery: IRequest<Result<ViewAccountResponse>>
    {
        // Should be used in the controller as input parameters
        public Guid AccountId { get; set; }
    }
}