﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Org.BouncyCastle.Crypto.Digests;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    public partial class Sha256Impl
    {
        private static async Task<string> DoGenerateInBase64Async(FileInfo file, CancellationToken cancellationToken)
        {
            return Core.Util.Convert.ToBase64String(await GetDigestInByteArrayAsync(
                    file,
                    cancellationToken
            ).ConfigureAwait(false));
        }

        private static async Task<string> DoGenerateInHexAsync(FileInfo file, CancellationToken cancellationToken)
        {
            return Core.Util.Convert.ToHexString(await GetDigestInByteArrayAsync(
                    file,
                    cancellationToken
            ).ConfigureAwait(false));
        }

        private static async Task<byte[]> GetDigestInByteArrayAsync(FileInfo file, CancellationToken cancellationToken)
        {
            using (var readStream = file.OpenRead())
            {
                var digest = new Sha256Digest();
                var output = new byte[digest.GetDigestSize()];
                var buffer = new byte[BufferSizeInByte];
                int read;
                while ((read = await readStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    digest.BlockUpdate(buffer, 0, read);
                }
                digest.DoFinal(output, 0);
                return output;
            }
        }

        protected override async Task<string> OnGenerateInBase64Async(FileInfo file, CancellationToken cancellationToken)
        {
            if (UsingBouncyCastleFirst)
            {
                return await DoGenerateInBase64Async(file, cancellationToken).ConfigureAwait(false);
            }

            try
            {
                return await DefaultSha256.DoGenerateInBase64Async(file, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    throw;
                }

                Logger.GetInstance(typeof(Sha256Impl)).Fatal("Generating checksum by system in async error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return await DoGenerateInBase64Async(file, cancellationToken).ConfigureAwait(false);
        }

        protected override async Task<string> OnGenerateInHexAsync(FileInfo file, CancellationToken cancellationToken)
        {
            if (UsingBouncyCastleFirst)
            {
                return await DoGenerateInHexAsync(file, cancellationToken).ConfigureAwait(false);
            }

            try
            {
                return await DefaultSha256.DoGenerateInHexAsync(file, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException)
                {
                    throw;
                }

                Logger.GetInstance(typeof(Sha256Impl)).Fatal("Generating checksum by system in async error: " + e);
                UsingBouncyCastleFirst = true;
            }
            return await DoGenerateInHexAsync(file, cancellationToken).ConfigureAwait(false);
        }
    }
}