using K4os.Hash.xxHash;
using System.Text;

namespace Reporting.WithHash;

public interface IHashProvider
{
    string GenerateHash(string input);
}

public class HashProvider : IHashProvider
{
    public string GenerateHash(string input)
    {
        // Use XXHash, CRC32, or MurmurHash for fast, compact hashes.
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        ulong hash = XXH64.DigestOf(bytes, 0, bytes.Length);
        return hash.ToString("X16");
    }
}
