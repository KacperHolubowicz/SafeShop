using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace SafeShop.Repository.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly int iterationPbkdf2;
        private readonly int keyBytes;

        public EncryptionService(IConfiguration configuration)
        {
            iterationPbkdf2 = int.Parse(configuration["Safety:Iterations"]);
            keyBytes = int.Parse(configuration["Safety:KeyBytes"]);
        }

        public Tuple<byte[], byte[]> HashPasswordWithoutPreGeneratedSalt(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: iterationPbkdf2,
                numBytesRequested: keyBytes
            );
            return new Tuple<byte[], byte[]>(hashed, salt);
        }

        public byte[] HashPasswordWithPreGeneratedSalt(string password, byte[] salt)
        {
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: iterationPbkdf2,
                numBytesRequested: keyBytes
            );
            return hashed;
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(salt);
            return salt;
        }
    }
}
