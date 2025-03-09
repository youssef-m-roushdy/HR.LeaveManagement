using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services;
using HR.LeaveManagement.MVC.Services.Base;
using HR.LeaveManagement.MVC.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Hanssens.Net; // Add this for LocalStorage
using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies; // Add this for AutoMapper

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClientFactory with NSwag Client
builder.Services.AddHttpClient<IClient, Client>()
    .AddTypedClient<IClient>((httpClient, sp) =>
    {
        var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
        return new Client(apiSettings.BaseUrl, httpClient); // Pass baseUrl properly
    });

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options => 
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Register LocalStorage as a singleton
builder.Services.AddSingleton<LocalStorage>();

// Register the services
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();