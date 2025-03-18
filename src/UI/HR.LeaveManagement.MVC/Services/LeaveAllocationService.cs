using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        private readonly IClient _httpClient;

        public LeaveAllocationService(IClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                await AddBearerToken();
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocation = new CreateLeaveAllocationDto() { LeaveTypeId = leaveTypeId };
                var apiResponse = await _client.LeaveAllocationsPOSTAsync(createLeaveAllocation);
                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationError += error + Environment.NewLine;
                    }
                }
                return response;

            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}