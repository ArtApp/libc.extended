using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace libc.extended.Encryption
{
    public class RsaEncryption
    {
        private readonly UnicodeEncoding _encoder = new UnicodeEncoding();
        private readonly string _privateKey;
        private readonly string _publicKey;

        public RsaEncryption(string publicKey, string privateKey)
        {
            if (string.IsNullOrEmpty(publicKey) ||
                string.IsNullOrEmpty(privateKey))
                throw new Exception(
                    "You must provide both public and private keys");

            _privateKey = privateKey;
            _publicKey = publicKey;
        }

        public string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(',');
            var dataByte = new byte[dataArray.Length];
            for (var i = 0; i < dataArray.Length; i++) dataByte[i] = Convert.ToByte(dataArray[i]);
            rsa.FromXmlString(_privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);

            return _encoder.GetString(decryptedByte);
        }

        public string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);

            var encryptedByteArray =
                rsa.Encrypt(dataToEncrypt, false).ToArray();

            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();

            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);
                if (item < length) sb.Append(",");
            }

            return sb.ToString();
        }
    }
}