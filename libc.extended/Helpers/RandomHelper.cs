using System;
namespace libc.extended.Helpers {
    public static class RandomHelper {
        /// <summary>
        ///     36 characters
        /// </summary>
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        /// <summary>
        ///     10 digits
        /// </summary>
        private const string Nums = "0123456789";
        private static readonly Random random = new Random();
        public static string Generate(int length) {
            var res = new char[length];
            for (var i = 0; i < res.Length; i++) {
                var index = random.Next(0, Chars.Length);
                res[i] = Chars[index];
            }
            return new string(res);
        }
        public static string GenerateNumber(int length) {
            var res = new char[length];
            for (var i = 0; i < res.Length; i++) {
                var index = random.Next(0, Nums.Length);
                res[i] = Nums[index];
            }
            return new string(res);
        }
        public static string GenerateUntil(int codeLength, Func<string, bool> uniquePredicate) {
            string code = null;
            var iteration = Math.Pow(Chars.Length, codeLength);
            for (var i = 0; i < iteration; i++) {
                code = Generate(codeLength);
                if (!uniquePredicate(code)) break;
            }
            return code;
        }
    }
}