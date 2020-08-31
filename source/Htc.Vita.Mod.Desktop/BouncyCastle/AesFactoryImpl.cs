using Htc.Vita.Core.Crypto;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    /// <summary>
    /// Class AesFactoryImpl.
    /// Implements the <see cref="AesFactory" />
    /// </summary>
    /// <seealso cref="AesFactory" />
    public class AesFactoryImpl : AesFactory
    {
        /// <inheritdoc />
        protected override Aes OnGet(
                Aes.CipherMode cipherMode,
                Aes.PaddingMode paddingMode)
        {
            return new AesImpl
            {
                    Cipher = cipherMode,
                    Padding = paddingMode
            };
        }
    }
}
