using Application.Common.Models.Account;

namespace Application.Common.Interfaces;
public interface ICurrentAccountService
{
    Guid Id { get; }
    GeneralAccountView Account { get; }
    bool HasPerm(string perm);
    string GetJwtToken();
    bool IsAdmin();
}
