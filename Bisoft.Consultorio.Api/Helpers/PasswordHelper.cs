// Dominio/Helpers/PasswordHelper.cs
using System;
using System.Security.Cryptography;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Helpers
{
    public static class PasswordHelper
    {
        // ✅ Método para hashear contraseña
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // ✅ Método para verificar contraseña
        public static bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordHash))
                return false;

            var hash = HashPassword(password);
            return hash == passwordHash;
        }

        // ✅ Método alternativo con salt (más seguro)
        public static string HashPasswordWithSalt(string password, string salt = null)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));

            // Si no se proporciona salt, generar uno
            if (string.IsNullOrEmpty(salt))
            {
                salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            }

            using var sha256 = SHA256.Create();
            var combined = password + salt;
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = sha256.ComputeHash(bytes);
            return $"{salt}:{Convert.ToBase64String(hash)}";
        }

        public static bool VerifyPasswordWithSalt(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
                return false;

            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            var salt = parts[0];
            var hash = parts[1];
            var computedHash = HashPasswordWithSalt(password, salt);
            return computedHash == storedHash;
        }
    }
}