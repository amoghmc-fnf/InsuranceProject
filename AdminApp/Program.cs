using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdminApp.Services;
using Microsoft.AspNetCore.Authentication;

namespace AdminApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register services with HTTP client
            AddHttpClients(builder);

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5180") // Replace with your frontend origin
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Configure authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Use CORS policy
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static async void AddHttpClients(WebApplicationBuilder builder)
        {
            var AdminApiUrl = builder.Configuration.GetSection("AdminApiUrl").Value;
            var PolicyApiUrl = builder.Configuration.GetSection("PolicyApiUrl").Value;
            var UserApiUrl = builder.Configuration.GetSection("UserApiUrl").Value;
            var JwtApiUrl = builder.Configuration.GetSection("JwtApiUrl").Value;
            builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(JwtApiUrl);
            });

            builder.Services.AddHttpClient<IAdminService, AdminService>(client =>
            {
                client.BaseAddress = new Uri(AdminApiUrl);
                //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InNlY3JldFBhc3N3b3JkIiwibmJmIjoxNzI0ODExNTI4LCJleHAiOjE3MjQ4OTc5MjgsImlhdCI6MTcyNDgxMTUyOH0.dmmEpR2OEmuA4YtlZGv0W16yOTAzK8mgazUfPxAWoKY");
            });

            builder.Services.AddHttpClient<IHospitalService, HospitalService>(client =>
            {
                client.BaseAddress = new Uri(AdminApiUrl);
            });

            builder.Services.AddHttpClient<IClaimService, ClaimService>(client =>
            {
                client.BaseAddress = new Uri(PolicyApiUrl);
            });

            builder.Services.AddHttpClient<IInsuranceTypeService, InsuranceTypeService>(client =>
            {
                client.BaseAddress = new Uri(PolicyApiUrl);
            });

            builder.Services.AddHttpClient<IInsuredPolicyService, InsuredPolicyService>(client =>
            {
                client.BaseAddress = new Uri(PolicyApiUrl);
            });

            builder.Services.AddHttpClient<IPolicyHolderService, PolicyHolderService>(client =>
            {
                client.BaseAddress = new Uri(UserApiUrl);
            });
            builder.Services.AddHttpClient<IInsuranceService, InsuranceService>(client =>
            {
                client.BaseAddress = new Uri(PolicyApiUrl);
            });
            builder.Services.AddHttpClient<IPolicyRequestService, PolicyRequestService>(client =>
            {
                client.BaseAddress = new Uri(PolicyApiUrl);
            });
            builder.Services.AddHttpClient<IInsuredService, InsuredService>(client =>
            {
                client.BaseAddress = new Uri(UserApiUrl);
            });
            builder.Services.AddHttpClient<IPaymentService, PaymentService>(client =>
            {
                client.BaseAddress = new Uri(AdminApiUrl);
            });
            builder.Services.AddHttpClient<IEmailRecordService, EmailRecordService> (client =>
            {
                client.BaseAddress = new Uri(AdminApiUrl);
            });
        }
    }
}