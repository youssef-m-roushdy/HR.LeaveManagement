using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Services.Base;

namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task LeaveRequestsGETAsync();
        Task LeaveRequestsPOSTAsync(CreateLeaveRequestDto body);
        Task LeaveRequestsGET2Async(int id);
        Task LeaveRequestsPUTAsync(int id, UpdateLeaveRequestDto body);
        Task LeaveRequestsDELETEAsync(int id);
        Task ChangeapprovalAsync(int id, ChangeLeaveRequestApprovalDto body);
    }
}