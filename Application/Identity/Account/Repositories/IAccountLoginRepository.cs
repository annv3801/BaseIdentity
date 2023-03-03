using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Identity;

namespace Application.Identity.Account.Repositories
{
    public interface IAccountLoginRepository: IRepository<AccountLogin>
    {
        /// <summary>
        /// Get account data from information external login 
        /// </summary>
        /// <param name="providerKey"></param>
        /// <param name="email"></param>
        /// <param name="loginProvider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Account?> GetAccountByAccountLogin(string email, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));
    }
}