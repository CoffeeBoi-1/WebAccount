using System.Collections.Generic;
using System.Threading.Tasks;
using Front.BaseClasses;
using Front.Models;

namespace Front.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountBase>> GetAccounts(int page);
        Task<AccountDetailedModel> GetDetailedAccount(int page);
    }
}