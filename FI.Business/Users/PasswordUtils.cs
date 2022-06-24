using System;
using System.Text;

namespace FI.Business.Users
{
    public static class PasswordUtils
    {
        public static string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }

        public static bool Verify(string hash, string password)
        {
            return Hash(password).Equals(hash);
        }
    }
}
