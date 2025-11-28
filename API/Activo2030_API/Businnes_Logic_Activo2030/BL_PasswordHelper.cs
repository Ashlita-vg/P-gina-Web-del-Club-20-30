using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Activo2030
{
    public static class PasswordHelper
    {
        // Crea un hash seguro con salt usando PBKDF2
        public static string HashPassword(string password)
        {
            // Salt de 16 bytes
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // Hash de 32 bytes usando PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // Guardamos: salt:hash en Base64
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        // Verifica si el password ingresado coincide con el hash almacenado
        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':', 2);
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHashBytes = Convert.FromBase64String(parts[1]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(storedHashBytes, computedHash);
        }
    }
}
