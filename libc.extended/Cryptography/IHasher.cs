namespace libc.extended.Cryptography {
    public interface IHasher {
        string Hash(string data);
        bool VerifyHash(string data, string hashedData);
    }
}