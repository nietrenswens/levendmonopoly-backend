using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LevendMonopoly.Api.Utils
{
    public static class Cryptography
    {
        public static string HashPassword(string password, byte[] salt)
        {
            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return passwordHash;
        }

        public static byte[] GenerateSalt()
        {
            byte[] passwordSalt = RandomNumberGenerator.GetBytes(128 / 8);
            return passwordSalt;
        }
    }
}