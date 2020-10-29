using System;

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
    }
}
