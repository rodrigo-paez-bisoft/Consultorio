namespace Bisoft.Consultorio.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError($"Error de argumento: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);

        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"Error de clave no encontrada: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error interno: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Ocurrió un error al registrar el doctor.");
        }
    }
}
