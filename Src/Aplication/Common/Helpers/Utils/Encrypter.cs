using System.Security.Cryptography;
using System.Text;

namespace Aplication.Common.Helpers.Utils
{
    public static class Encrypter
    {
        public static string EncryptSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                StringBuilder sb = new();

                for (int i = 0; i < bytes.Length; i++)
                    sb.Append(bytes[i].ToString("x2"));

                return sb.ToString();
            }
        } 
    }
}
