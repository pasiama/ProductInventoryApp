
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductInventoryApp.Configurations;
using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Repository;
using ProductInventoryApp.Services.Interfaces;
using ProductInventoryApp.Services.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
//using EntityFrameworkCore.UnitOfWork.Extensions;

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
            builder.Services.AddTransient<SupplierSeed>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            builder.Services.AddScoped<ISuppliersServices, SuppliersServices>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Inventory App",
                    Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter Token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
            });
            //Register db
            builder.Services.AddDbContext<ApplicationContext>(
                options => {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));

                });

            //configure jwt
            builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(jwt =>
                {

                    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
                    jwt.RequireHttpsMetadata = false;
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        RequireExpirationTime = false,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://hubtel.com",
                        ValidAudience = "http://hubtel.com",
                        
                    };
                });

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                   .AddEntityFrameworkStores<ApplicationContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();

            });

            var corsPolicyName = "Inventory App";
            builder.Services .AddCors(options => options
             .AddPolicy(corsPolicyName, policy => policy
             .AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod()));

            //ADD LOGGER
            builder.Host.UseSerilog((context, config) =>
            {
                config.MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Debug(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz} {CorrelationId} [{Level:u3}] {Username} {Message:lj}{Exception}{NewLine}]")
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz} {CorrelationId} [{Level:u3}] {Username} {Message:lj}{Exception}{NewLine}]");

            });




            var app = builder.Build();





            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}"
                );

            app.UseSerilogRequestLogging();
            app.Run();
        }
    }
}
