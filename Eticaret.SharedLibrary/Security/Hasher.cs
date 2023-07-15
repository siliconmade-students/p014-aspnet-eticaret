using System.Security.Cryptography;
using System.Text;

namespace Eticaret.SharedLibrary.Security
{
    public static class Hasher
    {
        public static string GenerateHash(string input, string salt)
        {
            using var alg = new HMACSHA256(GetBytes(salt));
            var result = alg.ComputeHash(GetBytes(input));
            return Convert.ToBase64String(result);
        }

        public static string GenerateSalt(int size = 8)
        {
            var rn = RandomNumberGenerator.GetBytes(size);
            return Convert.ToBase64String(rn);
        }

        private static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
