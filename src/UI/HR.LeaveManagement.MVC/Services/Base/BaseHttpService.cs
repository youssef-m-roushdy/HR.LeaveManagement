using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Contracts;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        private readonly ILocalStorageService _localStorageService;

        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation errors have occurred.", ValidationError = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The requested item could not be found.", Success = false };
            }
            else
            {
                return new Response<Guid>() { Message = ex.Message, Success = false };
            }
        }

        protected async Task AddBearerToken()
        {
            // Retrieve the JWT token from the cookie
            var token = await _localStorageService.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            // Set the Authorization header
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}