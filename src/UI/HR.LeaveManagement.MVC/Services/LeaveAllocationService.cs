using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
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

        public Task DeleteLeaveAllocation(LeaveAllocationVM leaveAllocation)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveAllocationVM> GetLeaveAllocationDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LeaveAllocationVM>> GetLeaveAllocations()
        {
            throw new NotImplementedException();
        }

        public Task UpdateLeaveAllocation(LeaveAllocationVM leaveAllocation)
        {
            throw new NotImplementedException();
        }
    }
}