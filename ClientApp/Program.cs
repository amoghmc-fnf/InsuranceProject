using MyClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

namespace MyClientApp.Client
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            Configuration = builder.Configuration;

            builder.Services.AddTransient(sp => new HttpClient());




            builder.Services.AddScoped<IInsuranceTypeService, InsuranceTypeService>();
            builder.Services.AddScoped<IPolicyHolderDtoService, PolicyHolderDtoService>();
            builder.Services.AddScoped<IInsuredService, InsuredService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPolicyService, PolicyService>();
            builder.Services.AddScoped<IInsuredPolicyService, InsuredPolicyService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();
            builder.Services.AddScoped<AuthService>();
            await builder.Build().RunAsync();
        }
    }
}
