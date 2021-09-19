using System;
using System.IO;
using System.Text;
using System.Threading;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Htc.Vita.Mod.Desktop.Crypto
{
    /// <summary>
    /// Class BouncyCastle.
    /// </summary>
    public static partial class BouncyCastle
    {
        /// <summary>
        /// Class AesFactory.
        /// Implements the <see cref="Core.Crypto.AesFactory" />
        /// </summary>
        /// <seealso cref="Core.Crypto.AesFactory" />
        public class AesFactory : Core.Crypto.AesFactory
        {
            /// <inheritdoc />
            protected override Core.Crypto.Aes OnGet(
                    Core.Crypto.Aes.CipherMode cipherMode,
                    Core.Crypto.Aes.PaddingMode paddingMode)
            {
                return new Aes
                {
                        Cipher = cipherMode,
                        Padding = paddingMode
                };
            }
        }

        /// <summary>
        /// Class Aes.
        /// Implements the <see cref="Core.Crypto.Aes" />
        /// </summary>
        /// <seealso cref="Core.Crypto.Aes" />
        public class Aes : Core.Crypto.Aes
        {
            private static IBlockCipher ConvertToImpl(CipherMode cipherMode)
            {
                if (cipherMode == CipherMode.Cbc)
                {
                    return new CbcBlockCipher(new AesEngine());
                }
                Logger.GetInstance(typeof(Aes)).Error($"unknown cipher mode: {cipherMode}");
                return new CbcBlockCipher(new AesEngine());
            }

            private static IBlockCipherPadding ConvertToImpl(PaddingMode paddingMode)
            {
                if (paddingMode == PaddingMode.Pkcs7)
                {
                    return new Pkcs7Padding();
                }
                Logger.GetInstance(typeof(Aes)).Error($"unknown padding mode: {paddingMode}");
                return new Pkcs7Padding();
            }

            /// <inheritdoc />
            protected override byte[] OnDecrypt(
                    byte[] input,
                    byte[] key,
                    byte[] iv)
            {
                if (iv == null || iv.Length != IvSize128BitInByte)
                {
                    throw new ArgumentException("iv size is not match");
                }

                if (key.Length != KeySize128BitInByte
                        && key.Length != KeySize192BitInByte
                        && key.Length != KeySize256BitInByte)
                {
                    throw new ArgumentException("key size is not match");
                }

                var parameters = new ParametersWithIV(
                        new KeyParameter(key),
                        iv
                );
                var cipher = new PaddedBufferedBlockCipher(
                        ConvertToImpl(Cipher),
                        ConvertToImpl(Padding)
                );
                cipher.Init(
                        forEncryption: false,
                        parameters: parameters
                );

                var bufferSize = cipher.GetOutputSize(input.Length);
                var buffer = new byte[bufferSize];
                var bufferUsage = cipher.ProcessBytes(
                        input,
                        0,
                        input.Length,
                        buffer,
                        0
                );
                bufferUsage += cipher.DoFinal(
                        buffer,
                        bufferUsage
                );

                if (bufferSize == bufferUsage)
                {
                    return buffer;
                }

                var result = new byte[bufferUsage];
                Array.Copy(
                        buffer,
                        result,
                        bufferUsage
                );
                return result;
            }

            /// <inheritdoc />
            protected override byte[] OnEncrypt(
                    byte[] input,
                    byte[] key,
                    byte[] iv)
            {
                if (iv == null || iv.Length != IvSize128BitInByte)
                {
                    throw new ArgumentException("iv size is not match");
                }

                if (key.Length != KeySize128BitInByte
                        && key.Length != KeySize192BitInByte
                        && key.Length != KeySize256BitInByte)
                {
                    throw new ArgumentException("key size is not match");
                }

                var parameters = new ParametersWithIV(
                        new KeyParameter(key),
                        iv
                );
                var cipher = new PaddedBufferedBlockCipher(
                        ConvertToImpl(Cipher),
                        ConvertToImpl(Padding)
                );
                cipher.Init(
                        forEncryption: true,
                        parameters: parameters
                );

                var bufferSize = cipher.GetOutputSize(input.Length);
                var buffer = new byte[bufferSize];
                var bufferUsage = cipher.ProcessBytes(
                        input,
                        0,
                        input.Length,
                        buffer,
                        0
                );
                bufferUsage += cipher.DoFinal(
                        buffer,
                        bufferUsage
                );

                if (bufferSize == bufferUsage)
                {
                    return buffer;
                }

                var result = new byte[bufferUsage];
                Array.Copy(
                        buffer,
                        result,
                        bufferUsage
                );
                return result;
            }
        }

        /// <summary>
        /// Class Md5.
        /// Implements the <see cref="Core.Crypto.Md5" />
        /// </summary>
        /// <seealso cref="Core.Crypto.Md5" />
        public partial class Md5 : Core.Crypto.Md5
        {
            private const int BufferSizeInByte = 1024 * 128;

            private static bool _usingBouncyCastleFirst;

            /// <summary>
            /// Gets or sets a value indicating whether using BouncyCastle first.
            /// </summary>
            /// <value><c>true</c> if using BouncyCastle first; otherwise, <c>false</c>.</value>
            public static bool UsingBouncyCastleFirst
            {
                get
                {
                    return _usingBouncyCastleFirst;
                }
                set
                {
                    if (_usingBouncyCastleFirst == value)
                    {
                        return;
                    }

                    _usingBouncyCastleFirst = value;
                    Logger.GetInstance(typeof(Md5)).Info($"Prefer using BouncyCastle first: {_usingBouncyCastleFirst}");
                }
            }

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
                    var digest = new MD5Digest();
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
                var digest = new MD5Digest();
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
                    return DefaultMd5.DoGenerateInBase64(
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

                    Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum by system error: {e}");
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
                    return DefaultMd5.DoGenerateInBase64(content);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum by system error: {e}");
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
                    return DefaultMd5.DoGenerateInHex(
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

                    Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum by system error: {e}");
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
                    return DefaultMd5.DoGenerateInHex(content);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Md5)).Fatal($"Generating checksum by system error: {e}");
                    UsingBouncyCastleFirst = true;
                }
                return DoGenerateInHex(content);
            }
        }

        /// <summary>
        /// Class Sha1.
        /// Implements the <see cref="Core.Crypto.Sha1" />
        /// </summary>
        /// <seealso cref="Core.Crypto.Sha1" />
        public partial class Sha1 : Core.Crypto.Sha1
        {
            private const int BufferSizeInByte = 1024 * 128;

            private static bool _usingBouncyCastleFirst;

            /// <summary>
            /// Gets or sets a value indicating whether using BouncyCastle first.
            /// </summary>
            /// <value><c>true</c> if using BouncyCastle first; otherwise, <c>false</c>.</value>
            public static bool UsingBouncyCastleFirst
            {
                get
                {
                    return _usingBouncyCastleFirst;
                }
                set
                {
                    if (_usingBouncyCastleFirst == value)
                    {
                        return;
                    }

                    _usingBouncyCastleFirst = value;
                    Logger.GetInstance(typeof(Sha1)).Info($"Prefer using BouncyCastle first: {_usingBouncyCastleFirst}");
                }
            }

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
                    var digest = new Sha1Digest();
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
                var digest = new Sha1Digest();
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
                    return DefaultSha1.DoGenerateInBase64(
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

                    Logger.GetInstance(typeof(Sha1)).Fatal($"Generating checksum by system error: {e}");
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
                    return DefaultSha1.DoGenerateInBase64(content);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Sha1)).Fatal($"Generating checksum by system error: {e}");
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
                    return DefaultSha1.DoGenerateInHex(
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

                    Logger.GetInstance(typeof(Sha1)).Fatal($"Generating checksum by system error: {e}");
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
                    return DefaultSha1.DoGenerateInHex(content);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Sha1)).Fatal($"Generating checksum by system error: {e}");
                    UsingBouncyCastleFirst = true;
                }
                return DoGenerateInHex(content);
            }
        }

        /// <summary>
        /// Class Sha256.
        /// Implements the <see cref="Htc.Vita.Core.Crypto.Sha256" />
        /// </summary>
        /// <seealso cref="Htc.Vita.Core.Crypto.Sha256" />
        public partial class Sha256 : Core.Crypto.Sha256
        {
            private const int BufferSizeInByte = 1024 * 128;

            private static bool _usingBouncyCastleFirst;

            /// <summary>
            /// Gets or sets a value indicating whether using BouncyCastle first.
            /// </summary>
            /// <value><c>true</c> if using BouncyCastle first; otherwise, <c>false</c>.</value>
            public static bool UsingBouncyCastleFirst
            {
                get
                {
                    return _usingBouncyCastleFirst;
                }
                set
                {
                    if (_usingBouncyCastleFirst == value)
                    {
                        return;
                    }

                    _usingBouncyCastleFirst = value;
                    Logger.GetInstance(typeof(Sha256)).Info($"Prefer using BouncyCastle first: {_usingBouncyCastleFirst}");
                }
            }

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

                    Logger.GetInstance(typeof(Sha256)).Fatal($"Generating checksum by system error: {e}");
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
                    Logger.GetInstance(typeof(Sha256)).Fatal($"Generating checksum by system error: {e}");
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

                    Logger.GetInstance(typeof(Sha256)).Fatal($"Generating checksum by system error: {e}");
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
                    Logger.GetInstance(typeof(Sha256)).Fatal($"Generating checksum by system error: {e}");
                    UsingBouncyCastleFirst = true;
                }
                return DoGenerateInHex(content);
            }
        }
    }
}
