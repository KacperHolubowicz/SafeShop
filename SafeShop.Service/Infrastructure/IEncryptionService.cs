using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.Infrastructure
{
    public interface IEncryptionService
    {
        Tuple<byte[], byte[]> HashPasswordWithoutPreGeneratedSalt(string password);
        byte[] HashPasswordWithPreGeneratedSalt(string password, byte[] salt);
        byte[] GenerateSalt();
    }
}
