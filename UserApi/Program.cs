using Microsoft.EntityFrameworkCore;
using UserDbService.Data;
using UserDbService.Services;

namespace UserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FnfProjectContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FnfProject"));
            });
            builder.Services.AddTransient<IPolicyHolderService, PolicyHolderService>();
            builder.Services.AddTransient<IInsuredService, InsuredService>();
            builder.Services.AddTransient<IIllnessService, IllnessService>();
            builder.Services.AddTransient<IInsuredIllnessService, InsuredIllnessService>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
