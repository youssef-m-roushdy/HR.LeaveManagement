using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services.Base;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveAllocationService : ILeaveAllocationService
    {
        private readonly IClient _client;

        public LeaveAllocationService(IClient client)
        {
            _client = client;
        }

        public async Task LeaveAllocationsGETAsync()
        {
            await _client.LeaveAllocationsGETAsync();
        }

        public async Task LeaveAllocationsPOSTAsync(CreateLeaveAllocationDto body)
        {
            await _client.LeaveAllocationsPOSTAsync(body);
        }

        public async Task LeaveAllocationsGET2Async(int id)
        {
            await _client.LeaveAllocationsGET2Async(id);
        }

        public async Task LeaveAllocationsPUTAsync(int id, UpdateLeaveAllocationDto body)
        {
            await _client.LeaveAllocationsPUTAsync(id, body);
        }

        public async Task LeaveAllocationsDELETEAsync(int id)
        {
            await _client.LeaveAllocationsDELETEAsync(id);
        }
    }
}