using System;
using System.IO;
using System.Threading.Tasks;
using Htc.Vita.Core.Crypto;
using Org.BouncyCastle.Crypto.Digests;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    public partial class Sha1Impl
    {
        private static async Task<string> DoGenerateInBase64Async(FileInfo file)
        {
            return Core.Util.Convert.ToBase64String(await GetDigestInByteArrayAsync(file).ConfigureAwait(false));
        }

        private static async Task<string> DoGenerateInHexAsync(FileInfo file)
        {
            return Core.Util.Convert.ToHexString(await GetDigestInByteArrayAsync(file).ConfigureAwait(false));
        }

        private static async Task<byte[]> GetDigestInByteArrayAsync(FileInfo file)
        {
            using (var readStream = file.OpenRead())
            {
                var digest = new Sha1Digest();
                var output = new byte[digest.GetDigestSize()];
                var buffer = new byte[131072]; // 128K
                int read;
                while ((read = await readStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                {
                    digest.BlockUpdate(buffer, 0, read);
                }
                digest.DoFinal(output, 0);
                return output;
            }
        }

        protected override async Task<string> OnGenerateInBase64Async(FileInfo file)
        {
            if (UsingBouncyCastleFirst)
            {
                return await DoGenerateInBase64Async(file).ConfigureAwait(false);
            }

            try
            {
                return await DefaultSha1.DoGenerateInBase64Async(file).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.Fatal("Generating checksum by system in async error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return await DoGenerateInBase64Async(file).ConfigureAwait(false);
        }

        protected override async Task<string> OnGenerateInHexAsync(FileInfo file)
        {
            if (UsingBouncyCastleFirst)
            {
                return await DoGenerateInHexAsync(file).ConfigureAwait(false);
            }

            try
            {
                return await DefaultSha1.DoGenerateInHexAsync(file).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.Fatal("Generating checksum by system in async error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return await DoGenerateInHexAsync(file).ConfigureAwait(false);
        }
    }
}
