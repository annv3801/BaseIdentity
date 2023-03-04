using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO.Jwt.Requests;
using Application.DTO.Jwt.Responses;
using Domain.Entities.Identity;

namespace Application.Common.Interfaces;
public interface IJwtService
{
    Task<Result<CreateJwtResponse>> GenerateJwtAsync(Account account, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken);
}