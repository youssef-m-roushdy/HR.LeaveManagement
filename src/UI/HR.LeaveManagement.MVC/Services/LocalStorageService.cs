using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Hanssens.Net;
using HR.LeaveManagement.MVC.Contracts;

namespace HR.LeaveManagement.MVC.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalStorageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task SetTokenAsync(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var expClaim = jwtToken.Payload.Exp;

            DateTimeOffset expirationTime = expClaim.HasValue
                ? DateTimeOffset.FromUnixTimeSeconds(expClaim.Value)
                : DateTimeOffset.UtcNow.AddMinutes(30); // Default if no expiration is set

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Prevent client-side script access
                Secure = true,   // Ensure the cookie is only sent over HTTPS
                Expires = expirationTime.UtcDateTime,
                SameSite = SameSiteMode.Strict // Prevent CSRF attacks
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("JWToken", token, cookieOptions);

            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["JWToken"];
            return Task.FromResult(token);
        }

        public Task RemoveTokenAsync()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("JWToken");
            return Task.CompletedTask;
        }
    }
}