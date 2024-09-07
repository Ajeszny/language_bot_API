using quadrolingoAPI.Models;
using Microsoft.EntityFrameworkCore; 

namespace quadrolingoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<APIContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("quadrolingoBaza")));

            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Allows all origins
                          .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, etc.)
                          .AllowAnyHeader(); // Allows all headers
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
