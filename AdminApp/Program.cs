using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdminApp.Services;
using CustomerApp.Services;

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
            builder.Services.AddHttpClient<IAdminService, AdminService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5101/api/");
            });

            builder.Services.AddHttpClient<IHospitalService, HospitalService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5101/api/");
            });

            builder.Services.AddHttpClient<IClaimService, ClaimService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5133/api/");
            });

            builder.Services.AddHttpClient<IPolicyHolderService, PolicyHolderService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5049/api/");
            });
            builder.Services.AddHttpClient<IInsuranceService, InsuranceService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5133/api/");
            });
            builder.Services.AddHttpClient<IPolicyRequestService, PolicyRequestService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5133/api/");
            });
            builder.Services.AddHttpClient<IInsuredDtoService, InsuredDtoService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5049/api/");
            });

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
    }
}