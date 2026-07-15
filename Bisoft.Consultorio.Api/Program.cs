
using Bisoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Service;
using Infraestructura.Context;
using Infraestructura.Repositories.Consultorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bisoft.Consultorio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                var connectionString = builder.Configuration["DatabaseConnections:Consultorio:ConnectionString"];
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("SQLite connection string is not configured. Check appsettings.json for DatabaseConnections:Consultorio:ConnectionString.");
                }

                builder.Services.AddScoped<DoctorService>();
                builder.Services.AddScoped<DoctorDomainService>();
                builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
                builder.Services.AddDbContext<ConsultorioContext>(

                    options => options.UseSqlite(connectionString)
                );
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.SQLite(
                        sqliteDbPath: "Logs/Logs.db",
                        tableName: "Logs",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                    ).CreateLogger();
                //builder.Services.Add

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

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapGet("/api/doctores/{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        try
                        {
                            var doctor = await doctorService.ConsultarDoctor(doctorId);
                            return Results.Ok(doctor);

                        }
                        catch (KeyNotFoundException ex)
                        {
                            Log.Error(ex, "Error al consultar doctor con id {DoctorId}", doctorId);
                            return Results.NotFound(new { message = ex.Message });
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Error al consultar doctor con id {DoctorId}", doctorId);
                            return Results.Problem("Ocurrió un error al consultar el doctor.");
                        }
                    }
                )
                    .WithSummary("Consultar Doctor")
                    .WithName("Consultar Doctor");


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
