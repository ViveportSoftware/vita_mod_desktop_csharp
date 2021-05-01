using System;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    /// <summary>
    /// Class AesImpl.
    /// Implements the <see cref="Aes" />
    /// </summary>
    /// <seealso cref="Aes" />
    public class AesImpl : Aes
    {
        private static IBlockCipher ConvertToImpl(CipherMode cipherMode)
        {
            if (cipherMode == CipherMode.Cbc)
            {
                return new CbcBlockCipher(new AesEngine());
            }
            Logger.GetInstance(typeof(AesImpl)).Error($"unknown cipher mode: {cipherMode}");
            return new CbcBlockCipher(new AesEngine());
        }

        private static IBlockCipherPadding ConvertToImpl(PaddingMode paddingMode)
        {
            if (paddingMode == PaddingMode.Pkcs7)
            {
                return new Pkcs7Padding();
            }
            Logger.GetInstance(typeof(AesImpl)).Error($"unknown padding mode: {paddingMode}");
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
}
