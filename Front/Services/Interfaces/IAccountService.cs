using AccountLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Front.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountModel>> GetAccounts(int page);
        Task<AccountDetailedModel> GetDetailedAccount(int page);
    }
}