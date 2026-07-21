using Bisoft.Consultorio.Api.DTOs.Configurations;
using Bisoft.Consultorio.Api.DTOs.Doctor;
using Bisoft.Consultorio.Api.Extensions;
using Bisoft.Consultorio.Api.Extensions.Endponints;
using Bisoft.Consultorio.Api.Middlewares;
using Bisoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Service;
using BiSoft.Consultorio.Infraestructura.Context;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bisoft.Consultorio.Api
{
    public class Program
    {
        public const string RATE_LIMITER_POLICY_NAME = "FIXED";
        public const string CORS_POLICY_NAME = "AllowAll";
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                var configuration = builder.Configuration.GetGeneralConfigurations();

                builder.Services.AddSingleton(configuration.JWT);
                //Inyeccion de servicios
                builder.Services.InyectarServicios()
                                .ConfigurarSwagger()
                                .ConfigurarCors()
                                .InyectarContextos(configuration.ConnectionString) 
                                .ConfigurarHealthChecks(configuration.ConnectionString)
                                .ConfigureRateLimiter(configuration.RateLimit)
                                .configureLogger();
                //
              
                // Add services to the container.
                builder.Services.AddAuthorization();

                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();

                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ConsultorioContext>();
                    context.Database.EnsureCreated();
                }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }
                

                app.UseCors(CORS_POLICY_NAME);
                app.UseHttpsRedirection();
                app.UseAuthorization();
                                    
                //OpenApi
                app.UseSwagger();
                app.UseSwaggerUI();
                //Cors
                app.UseMiddleware<ErrorHandlerMiddleware>();
                app.AddHealthChecks(RATE_LIMITER_POLICY_NAME);
                app.MapEndpoints();
                app.UseRateLimiter();
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application start-up failed {ex.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
