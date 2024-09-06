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

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
