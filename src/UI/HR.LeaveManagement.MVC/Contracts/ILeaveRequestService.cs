using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestVM>> GetLeaveRequests();
        Task<LeaveRequestVM> GetLeaveRequestDetails(int id);
        //Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);

        Task UpdateLeaveRequest(LeaveRequestVM leaveRequest);
        Task DeleteLeaveRequest(LeaveRequestVM leaveRequest);
    }
}