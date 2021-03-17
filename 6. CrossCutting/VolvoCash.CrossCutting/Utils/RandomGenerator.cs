using System;
using System.Linq;

namespace VolvoCash.CrossCutting.Utils
{
    public static class RandomGenerator
    {
        public static string RandomDigits(int length)
        {
            var random = new Random();
            var code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                code = string.Concat(code, random.Next(1,9).ToString());
            }
            return code;
        }
        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
