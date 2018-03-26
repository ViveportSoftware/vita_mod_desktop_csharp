using System;
using System.IO;
using System.Text;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Org.BouncyCastle.Crypto.Digests;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    public partial class Md5Impl : Md5
    {
        private static bool UsingBouncyCastleFirst { get; set; } = true;

        private readonly Logger _logger;

        public Md5Impl()
        {
            _logger = Logger.GetInstance();
        }

        private static string DoGenerateInBase64(FileInfo file)
        {
            return Core.Util.Convert.ToBase64String(GetDigestInByteArray(file));
        }

        private static string DoGenerateInBase64(string content)
        {
            return Core.Util.Convert.ToBase64String(GetDigestInByteArray(content));
        }

        private static string DoGenerateInHex(FileInfo file)
        {
            return Core.Util.Convert.ToHexString(GetDigestInByteArray(file));
        }

        private static string DoGenerateInHex(string content)
        {
            return Core.Util.Convert.ToHexString(GetDigestInByteArray(content));
        }

        private static byte[] GetDigestInByteArray(FileInfo file)
        {
            using (var readStream = file.OpenRead())
            {
                var digest = new MD5Digest();
                var output = new byte[digest.GetDigestSize()];
                var buffer = new byte[131072]; // 128K
                int read;
                while ((read = readStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    digest.BlockUpdate(buffer, 0, read);
                }
                digest.DoFinal(output, 0);
                return output;
            }
        }

        private static byte[] GetDigestInByteArray(string content)
        {
            var digest = new MD5Digest();
            var input = Encoding.UTF8.GetBytes(content);
            var output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(input, 0, input.Length);
            digest.DoFinal(output, 0);
            return output;
        }

        protected override string OnGenerateInBase64(FileInfo file)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInBase64(file);
            }

            try
            {
                return DefaultMd5.DoGenerateInBase64(file);
            }
            catch (Exception e)
            {
                _logger.Fatal("Generating checksum by system error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInBase64(file);
        }

        protected override string OnGenerateInBase64(string content)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInBase64(content);
            }

            try
            {
                return DefaultMd5.DoGenerateInBase64(content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Generating checksum by system error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInBase64(content);
        }

        protected override string OnGenerateInHex(FileInfo file)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInHex(file);
            }

            try
            {
                return DefaultMd5.DoGenerateInHex(file);
            }
            catch (Exception e)
            {
                _logger.Fatal("Generating checksum by system error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInHex(file);
        }

        protected override string OnGenerateInHex(string content)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInHex(content);
            }

            try
            {
                return DefaultMd5.DoGenerateInHex(content);
            }
            catch (Exception e)
            {
                _logger.Fatal("Generating checksum by system error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInHex(content);
        }
    }
}
