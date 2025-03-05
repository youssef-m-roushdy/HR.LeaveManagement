using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task LeaveAllocationsGETAsync();
        Task LeaveAllocationsPOSTAsync(CreateLeaveAllocationDto body);
        Task LeaveAllocationsGET2Async(int id);
        Task LeaveAllocationsPUTAsync(int id, UpdateLeaveAllocationDto body);
        Task LeaveAllocationsDELETEAsync(int id);
    }
}