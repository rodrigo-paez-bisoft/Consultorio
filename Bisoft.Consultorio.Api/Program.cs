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

                // ===== INYECCIÓN DE SERVICIOS =====
                builder.Services.InyectarServicios()
                                .ConfigurarSwagger()
                                .ConfigurarCors()
                                .InyectarContextos(
                                    configuration.ConnectionStringConsultorio,
                                    configuration.ConnectionStringSeguridad)
                                .ConfigurarHealthChecks(configuration.ConnectionStringConsultorio)
                                .ConfigureRateLimiter(configuration.RateLimit)
                                .configureLogger();

                // ===== AUTENTICACIÓN Y AUTORIZACIÓN =====
                builder.Services.AddAuthorization();
                builder.Services.ConfigureAuthentication(configuration.JWT);

                builder.Services.AddOpenApi();

                var app = builder.Build();

                // ===== BASE DE DATOS =====
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ConsultorioContext>();
                    context.Database.EnsureCreated();
                }

                // ===== PIPELINE DE MIDDLEWARES =====

                // 1. Configuración de entorno
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                // 2. 🔽 SWAGGER - DEBE IR ANTES DE CUALQUIER MIDDLEWARE DE AUTENTICACIÓN
                app.UseSwagger();
                app.UseSwaggerUI();

                // 3. CORS
                app.UseCors(CORS_POLICY_NAME);

                // 4. HTTPS
                app.UseHttpsRedirection();

                // 5. 🔽 AUTENTICACIÓN - ANTES DE UseAuthorization
                app.UseAuthentication();
                app.UseAuthorization();

                // 6. MIDDLEWARES PERSONALIZADOS
                app.UseMiddleware<ErrorHandlerMiddleware>();

                // 7. HEALTH CHECKS Y ENDPOINTS
                app.AddHealthChecks(RATE_LIMITER_POLICY_NAME);
                app.MapEndpoints();

                // 8. RATE LIMITER - DESPUÉS DE LOS ENDPOINTS
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