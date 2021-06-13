using System;
using System.Security.Cryptography;
using System.Text;
using libc.extended.Encryption;

namespace libc.extended.Cryptography
{
    public class Hash_SHA512 : IHasher
    {
        private readonly Func<byte[], string> byteArrayToString;

        public Hash_SHA512()
        {
            byteArrayToString = Base62.ToBase62;
        }

        public Hash_SHA512(Func<byte[], string> byteArrayToString)
        {
            this.byteArrayToString = byteArrayToString;
        }

        public string Hash(string data)
        {
            var input = Encoding.UTF8.GetBytes(data);
            byte[] hash;

            using (var sha = new SHA512Managed())
            {
                hash = sha.ComputeHash(input);
            }

            var output = byteArrayToString == null
                ? Encoding.UTF8.GetString(hash)
                : byteArrayToString(hash);

            return output;
        }

        public bool VerifyHash(string data, string hashedData)
        {
            return Hash(data) == hashedData;
        }
    }
}