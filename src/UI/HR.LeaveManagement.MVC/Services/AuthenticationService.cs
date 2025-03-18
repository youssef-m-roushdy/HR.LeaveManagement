using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HR.LeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalStorageService _localStorageService;
        private JwtSecurityTokenHandler _tokenHandler;
        public AuthenticationService(IClient client, ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        : base(client, localStorageService)
        {
            _localStorageService = localStorageService;
            _tokenHandler = new JwtSecurityTokenHandler();
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);

                if (!string.IsNullOrEmpty(authenticationResponse.Token))
                {
                    // Store JWT token in a secure cookie
                    await _localStorageService.SetTokenAsync(authenticationResponse.Token);

                    // Extract claims from JWT token
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);

                    // Create a ClaimsPrincipal for cookie-based authentication
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    // Sign in the user
                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            // Remove the JWT token cookie
            await _localStorageService.RemoveTokenAsync();

            // Sign out the cookie-based authentication session
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterVM registration)
        {
            try
            {
                // Map the registration data to the request object
                RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);

                // Call the API to register the user
                var response = await _client.RegisterAsync(registrationRequest);

                // Check if the response is null
                if (response == null)
                {
                    Console.WriteLine("1 failed try");
                    // Log the error or handle the null response case
                    return false;
                }

                // Check if the Id property is null or empty
                if (string.IsNullOrEmpty(response.UserId?.ToString()))
                {
                    Console.WriteLine("2 failed try");
                    // Log the error or handle the case where Id is null
                    return false;
                }

                // Registration was successful
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework)
                // Example: _logger.LogError(ex, "An error occurred during registration.");
                return false;
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}