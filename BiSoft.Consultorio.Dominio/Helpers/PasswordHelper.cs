using System;
using System.Security.Cryptography;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            var result = Convert.ToBase64String(hash);

            Console.WriteLine($"Hash generado para '{password}': '{result}'");

            return result;
        }

        public static bool VerifyPassword(string password, string hash)
        {
            var passwordHash = HashPassword(password);
            return passwordHash == hash;
        }
    }
}