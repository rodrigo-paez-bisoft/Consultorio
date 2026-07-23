// App/DTOs/Security/LoginRequest.cs
namespace Bisoft.Consultorio.Api.DTOs.Security
{
    public class LoginRequest
    {
        // ✅ Usar "Username" en lugar de "Usuario" para que coincida con tu Login.cs
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}