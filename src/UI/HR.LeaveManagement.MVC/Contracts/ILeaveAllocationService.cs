using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}