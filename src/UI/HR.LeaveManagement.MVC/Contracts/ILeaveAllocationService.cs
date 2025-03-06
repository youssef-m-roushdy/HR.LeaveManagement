using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<List<LeaveAllocationVM>> GetLeaveAllocations();
        Task<LeaveAllocationVM> GetLeaveAllocationDetails(int id);
        //Task<Response<int>> CreateLeaveAllocation(CreateLeaveAllocationVM leaveAllocation);

        Task UpdateLeaveAllocation(LeaveAllocationVM leaveAllocation);
        Task DeleteLeaveAllocation(LeaveAllocationVM leaveAllocation);
    }
}