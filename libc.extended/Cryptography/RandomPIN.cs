using System;

namespace libc.extended.Cryptography
{
    public static class RandomPIN
    {
        public static long GeneratePIN(Random r, int length)
        {
            var res = 0L;
            for (var i = 0; i < length; i++) res += (long) Math.Pow(10, i) * r.Next(0, 9);

            return res;
        }
    }
}