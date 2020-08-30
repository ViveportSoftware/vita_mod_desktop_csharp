using System;
using System.IO;
using System.Text;
using System.Threading;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Org.BouncyCastle.Crypto.Digests;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    /// <summary>
    /// Class Sha256Impl.
    /// Implements the <see cref="Sha256" />
    /// </summary>
    /// <seealso cref="Sha256" />
    public partial class Sha256Impl : Sha256
    {
        private const int BufferSizeInByte = 1024 * 128;

        private static bool UsingBouncyCastleFirst { get; set; } = true;

        private static string DoGenerateInBase64(
                FileInfo file,
                CancellationToken cancellationToken)
        {
            return Core.Util.Convert.ToBase64String(GetDigestInByteArray(
                    file,
                    cancellationToken
            ));
        }

        private static string DoGenerateInBase64(string content)
        {
            return Core.Util.Convert.ToBase64String(GetDigestInByteArray(content));
        }

        private static string DoGenerateInHex(
                FileInfo file,
                CancellationToken cancellationToken)
        {
            return Core.Util.Convert.ToHexString(GetDigestInByteArray(
                    file,
                    cancellationToken
            ));
        }

        private static string DoGenerateInHex(string content)
        {
            return Core.Util.Convert.ToHexString(GetDigestInByteArray(content));
        }

        private static byte[] GetDigestInByteArray(
                FileInfo file,
                CancellationToken cancellationToken)
        {
            using (var readStream = file.OpenRead())
            {
                var digest = new Sha256Digest();
                var output = new byte[digest.GetDigestSize()];
                var buffer = new byte[BufferSizeInByte];
                int read;
                while ((read = readStream.Read(
                        buffer,
                        0,
                        buffer.Length)) > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    digest.BlockUpdate(
                            buffer,
                            0,
                            read
                    );
                }
                digest.DoFinal(
                        output,
                        0
                );
                return output;
            }
        }

        private static byte[] GetDigestInByteArray(string content)
        {
            var digest = new Sha256Digest();
            var input = Encoding.UTF8.GetBytes(content);
            var output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(
                    input,
                    0,
                    input.Length
            );
            digest.DoFinal(
                    output,
                    0
            );
            return output;
        }

        /// <inheritdoc />
        protected override string OnGenerateInBase64(
                FileInfo file,
                CancellationToken cancellationToken)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInBase64(
                        file,
                        cancellationToken
                );
            }

            try
            {
                return DefaultSha256.DoGenerateInBase64(
                        file,
                        cancellationToken
                );
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    throw;
                }

                Logger.GetInstance(typeof(Sha256Impl)).Fatal($"Generating checksum by system error: {e}");
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInBase64(
                    file,
                    cancellationToken
            );
        }

        /// <inheritdoc />
        protected override string OnGenerateInBase64(string content)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInBase64(content);
            }

            try
            {
                return DefaultSha256.DoGenerateInBase64(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha256Impl)).Fatal($"Generating checksum by system error: {e}");
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInBase64(content);
        }

        /// <inheritdoc />
        protected override string OnGenerateInHex(
                FileInfo file,
                CancellationToken cancellationToken)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInHex(
                        file,
                        cancellationToken
                );
            }

            try
            {
                return DefaultSha256.DoGenerateInHex(
                        file,
                        cancellationToken
                );
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    throw;
                }

                Logger.GetInstance(typeof(Sha256Impl)).Fatal($"Generating checksum by system error: {e}");
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInHex(
                    file,
                    cancellationToken
            );
        }

        /// <inheritdoc />
        protected override string OnGenerateInHex(string content)
        {
            if (UsingBouncyCastleFirst)
            {
                return DoGenerateInHex(content);
            }

            try
            {
                return DefaultSha256.DoGenerateInHex(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Sha256Impl)).Fatal($"Generating checksum by system error: {e}");
                UsingBouncyCastleFirst = true;
            }
            return DoGenerateInHex(content);
        }
    }
}
