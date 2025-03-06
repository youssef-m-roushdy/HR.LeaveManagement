using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IClient _client;

        public LeaveRequestService(IClient client)
        {
            _client = client;
        }

        public Task DeleteLeaveRequest(LeaveRequestVM leaveRequest)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequestVM> GetLeaveRequestDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LeaveRequestVM>> GetLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public Task UpdateLeaveRequest(LeaveRequestVM leaveRequest)
        {
            throw new NotImplementedException();
        }
    }
}