using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceSite.Security
{
    public class Salts
    {
        private const int SALT_LENGTH = 16;
        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(SALT_LENGTH);
        }

        public static string HashPassword(string password, byte[] salt) 
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                return Convert.ToBase64String(hashedBytes);
            }
        }


    }
}
