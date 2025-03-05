using HR.LeaveManagement.MVC.Contracts;
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

        public async Task LeaveRequestsGETAsync()
        {
            await _client.LeaveRequestsGETAsync();
        }

        public async Task LeaveRequestsPOSTAsync(CreateLeaveRequestDto body)
        {
            await _client.LeaveRequestsPOSTAsync(body);
        }

        public async Task LeaveRequestsGET2Async(int id)
        {
            await _client.LeaveRequestsGET2Async(id);
        }

        public async Task LeaveRequestsPUTAsync(int id, UpdateLeaveRequestDto body)
        {
            await _client.LeaveRequestsPUTAsync(id, body);
        }

        public async Task LeaveRequestsDELETEAsync(int id)
        {
            await _client.LeaveRequestsDELETEAsync(id);
        }

        public async Task ChangeapprovalAsync(int id, ChangeLeaveRequestApprovalDto body)
        {
            await _client.ChangeapprovalAsync(id, body);
        }
    }
}