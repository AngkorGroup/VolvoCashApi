using System;
using System.Security.Cryptography;
using System.Text;

namespace VolvoCash.CrossCutting.Utils
{
    public static class CryptoMethods
    {
        public static string HashText(string text)
        {
            var hashProvider = new SHA256Managed();
            var shaData = hashProvider.ComputeHash(Encoding.ASCII.GetBytes(text));
            return BitConverter.ToString(shaData);
        }

        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = Encoding.ASCII.GetString(b);
            }
            catch
            {
                decrypted = "";
            }
            return decrypted;
        }

        public static string EncryptString(string strEncrypted)
        {
            byte[] b = Encoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}
