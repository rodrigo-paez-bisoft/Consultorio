using Bisoft.Consultorio.Api.DTOs.Configurations;
using Bisoft.Consultorio.Api.Helpers.HealthChecks;
using Bisoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Service;
using BiSoft.Consultorio.Infraestructura.Context;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;
using System.Threading.RateLimiting;

namespace Bisoft.Consultorio.Api.Extensions
{
    public static class ServiceExtensions
    {
        // ===== INYECCIÓN DE SERVICIOS =====
        public static IServiceCollection InyectarServicios(this IServiceCollection services)
        {
            // Doctores
            services.AddScoped<DoctorService>();
            services.AddScoped<DoctorDomainService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            // Pacientes
            services.AddScoped<PacienteService>();
            services.AddScoped<PacienteDomainService>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();

            // Salas
            services.AddScoped<SalaService>();
            services.AddScoped<SalaDomainService>();
            services.AddScoped<ISalaRepository, SalaRepository>();

            // Citas
            services.AddScoped<CitaService>();
            services.AddScoped<CitaDomainService>();
            services.AddScoped<ICitaRepository, CitaRepository>();

            // Usuarios
            services.AddScoped<UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Login Audit
            services.AddScoped<LoginAuditService>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();

            return services;
        }

        // ===== CONTEXTOS =====
        public static IServiceCollection InyectarContextos(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ConsultorioContext>(options =>
                options.UseSqlite(connectionString)
            );
            return services;
        }

        // ===== SWAGGER =====
        public static IServiceCollection ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        // ===== CORS =====
        public static IServiceCollection ConfigurarCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Program.CORS_POLICY_NAME,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            return services;
        }

        // ===== HEALTH CHECKS =====
        public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks()
                .AddCheck("Liveness", () => HealthCheckResult.Healthy($"API jalando al cien"))
                .AddCheck("Database", new DatabaseHealthCheck(connectionString), tags: ["ready"]);
            return services;
        }

        // ===== RATE LIMITER =====
        public static IServiceCollection ConfigureRateLimiter(this IServiceCollection services, int allowedRequestPerMinute)
        {
            services.AddRateLimiter(config =>
            {
                config.OnRejected = (context, ct) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                        ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                    }
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.WriteAsync("Demasiados request. Intente mas tarde.", cancellationToken: ct);
                    return new ValueTask();
                };

                config.AddFixedWindowLimiter(Program.RATE_LIMITER_POLICY_NAME, option =>
                {
                    option.PermitLimit = allowedRequestPerMinute;
                    option.Window = TimeSpan.FromMinutes(1);
                    option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    option.QueueLimit = 0;
                });
            });

            return services;
        }

        // ===== LOGGER =====
        public static IServiceCollection configureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                  .WriteTo.SQLite(
                      sqliteDbPath: "Logs/Logs.db",
                      tableName: "Logs",
                      restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                  )
                  .WriteTo.Console()
                  .CreateLogger();
            services.AddSerilog();
            return services;
        }

        // ===== AUTENTICACIÓN JWT =====
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, JwtConfiguration jwtConfiguration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secretkey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }
}