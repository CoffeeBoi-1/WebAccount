using System.Collections.Generic;
using System.Threading.Tasks;
using Front.BaseClasses;

namespace Front.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountBase>> GetAccounts(int page);
    }
}