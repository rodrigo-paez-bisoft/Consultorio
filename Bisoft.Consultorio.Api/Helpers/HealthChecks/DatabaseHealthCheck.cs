using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bisoft.Consultorio.Api.Helpers.HealthChecks
{
    
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;
        public DatabaseHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var canConnect = CanConnectToDatabase();
            if (!canConnect)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, description: "Pues no se pudo conectar a la base de datos papu");
            }
            return new HealthCheckResult(HealthStatus.Healthy, description: "Se pudo conectar a la base de datos papu");

        }
        private  bool CanConnectToDatabase()
        {
            try 
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                var canConnect = connection.State == System.Data.ConnectionState.Open;
                connection.Dispose();
                return true;
            } catch { return false; }
        }
    }
}
