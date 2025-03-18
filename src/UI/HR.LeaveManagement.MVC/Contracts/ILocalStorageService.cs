using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILocalStorageService
    {
        Task SetTokenAsync(string token);
        Task<string?> GetTokenAsync();
        Task RemoveTokenAsync();
    }
}