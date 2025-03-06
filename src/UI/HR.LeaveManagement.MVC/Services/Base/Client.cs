using System;
using System.Net.Http;
using HR.LeaveManagement.MVC.Settings;
using Microsoft.Extensions.Options;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public partial class Client : IClient
    {
        // Custom constructor that uses the existing _httpClient field from NSwag
        public Client(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : this(apiSettings.Value.BaseUrl, httpClient)
        {
            InitializePartial();
        }

        public HttpClient HttpClient => _httpClient; // Use NSwag's existing _httpClient

        // Partial method for any extra initialization
        partial void InitializePartial();
    }
}
