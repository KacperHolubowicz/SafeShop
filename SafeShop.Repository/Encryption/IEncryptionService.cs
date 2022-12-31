namespace SafeShop.Repository.Encryption
{
    public interface IEncryptionService
    {
        Tuple<byte[], byte[]> HashPasswordWithoutPreGeneratedSalt(string password);
        byte[] HashPasswordWithPreGeneratedSalt(string password, byte[] salt);
        byte[] GenerateSalt();
    }
}
