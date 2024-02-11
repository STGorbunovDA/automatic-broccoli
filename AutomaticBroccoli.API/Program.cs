using AutomaticBroccoli.DataAccess.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutomaticBroccoli.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "AutomaticBroccoli.API.xml");
                option.IncludeXmlComments(filePath);
            });

            builder.Services.AddDbContext<AutomaticBroccoliDbContext>(options =>
            {
                options
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            
        }
    }
}

public partial class Program { }
