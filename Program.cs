
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Repository;

namespace ProductInventoryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

          


            builder.Services.AddControllers();
            builder.Services.AddTransient<Seed>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Register db
            builder.Services.AddDbContext<ApplicationContext>(
                options => {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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

            app.MapControllerRoute(
                name: "default",
                pattern:"{controller=Products}/{action=Index}/{id?}"
                );


            app.Run();
        }
    }
}
