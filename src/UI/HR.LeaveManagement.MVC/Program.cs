using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services;
using HR.LeaveManagement.MVC.Services.Base;
using HR.LeaveManagement.MVC.Settings;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClientFactory with NSwag Client
builder.Services.AddHttpClient<IClient, Client>()
    .AddTypedClient<IClient>((httpClient, sp) =>
    {
        var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
        return new Client(apiSettings.BaseUrl, httpClient);
    });

builder.Services.AddHttpContextAccessor();

// Configure authentication & cookie settings
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.LoginPath = "/Account/Login"; // Redirect to login page if unauthorized
            options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect to access denied page
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

// Register application services
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 🔹 Moved before Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
