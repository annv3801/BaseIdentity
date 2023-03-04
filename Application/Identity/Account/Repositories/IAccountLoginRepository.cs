using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Identity;

namespace Application.Identity.Account.Repositories;
public interface IAccountLoginRepository: IRepository<AccountLogin>
{
    Task<Domain.Entities.Identity.Account?> GetAccountByAccountLogin(string email, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));
}
