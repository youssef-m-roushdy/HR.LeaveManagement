using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.MVC.Settings;
using Microsoft.Extensions.Options;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public partial class Client : IClient
    {
        private readonly ApiSettings _apiSettings;

        // Use the existing _httpClient field from the generated code
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }

        public Client(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }
    }
}