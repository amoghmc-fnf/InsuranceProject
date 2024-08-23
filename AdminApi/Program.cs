using AdminDbService.Data;
using AdminDbService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdminApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureCors(builder);
            ConfigureDbContextServices(builder);
            ConfigureAuthentication(builder);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("cors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            var apiKey = builder.Configuration["JwtApiKey"];
            var key = Encoding.ASCII.GetBytes(apiKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        private static void ConfigureDbContextServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<FnfProjectContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FnfProject"));
            });
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IBlacklistService, BlacklistService>();
            builder.Services.AddTransient<IHospitalService, HospitalService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();
            builder.Services.AddTransient<IEmailRecordService, EmailRecordService>();
        }

        private static void ConfigureCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(setUpAction =>
            {
                setUpAction.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
        }
    }
}
