using Htc.Vita.Core.Crypto;

namespace Htc.Vita.Mod.Desktop.BouncyCastle
{
    public class AesFactoryImpl : AesFactory
    {
        protected override Aes OnGet(Aes.CipherMode cipherMode, Aes.PaddingMode paddingMode)
        {
            return new AesImpl
            {
                    Cipher = cipherMode,
                    Padding = paddingMode
            };
        }
    }
}
