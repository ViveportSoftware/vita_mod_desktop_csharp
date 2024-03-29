using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Htc.Vita.Mod.Desktop.Tests
{
    public static class Md5Test
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
        }

        [Fact]
        public static void Default_1_GenerateInBase64_WithContent()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var value = md5.GenerateInBase64("");
            Assert.Equal("1B2M2Y8AsgTpgAmY7PhCfg==", value);
            var value2 = md5.GenerateInBase64("123");
            Assert.Equal("ICy5YqxZB1uWSwcVLSNLcA==", value2);
        }

        [Fact]
        public static void Default_1_GenerateInBase64_WithFile()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var path = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            if (!Core.Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Md5.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("pq/Xu7jVnluxLJ28xOws/w==", md5.GenerateInBase64(file));
        }

        [Fact(Skip = "Need large test data")]
        public static void Default_1_GenerateInBase64_WithFile_withCancellationToken()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            const string path = @"%USERPROFILE%\Downloads\en_windows_10_consumer_editions_version_2004_x64_dvd_8d28c5d7.iso";
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            var cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                    cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(2));
            }, CancellationToken.None);
            var result = md5.GenerateInBase64Async(file, cancellationTokenSource.Token).Result;
            Assert.True(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public static void Default_2_ValidateInBase64_WithContent()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            Assert.True(md5.ValidateInBase64("", "1B2M2Y8AsgTpgAmY7PhCfg=="));
            Assert.True(md5.ValidateInBase64("123", "ICy5YqxZB1uWSwcVLSNLcA=="));
        }

        [Fact]
        public static void Default_2_ValidateInBase64_WithFile()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var path = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            if (!Core.Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Md5.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(md5.ValidateInBase64(file, "pq/Xu7jVnluxLJ28xOws/w=="));
        }

        [Fact]
        public static void Default_3_GenerateInHex_WithContent()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var value = md5.GenerateInHex("");
            Assert.Equal("d41d8cd98f00b204e9800998ecf8427e", value);
            var value2 = md5.GenerateInHex("123");
            Assert.Equal("202cb962ac59075b964b07152d234b70", value2);
        }

        [Fact]
        public static void Default_3_GenerateInHex_WithFile()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var path = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            if (!Core.Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Md5.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.Equal("a6afd7bbb8d59e5bb12c9dbcc4ec2cff", md5.GenerateInHex(file));
        }

        [Fact]
        public static void Default_4_ValidateInHex_WithContent()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            Assert.True(md5.ValidateInHex("", "d41d8cd98f00b204e9800998ecf8427e"));
            Assert.True(md5.ValidateInHex("123", "202cb962ac59075b964b07152d234b70"));
        }

        [Fact]
        public static void Default_4_ValidateInHex_WithFile()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var path = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            if (!Core.Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Md5.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(md5.ValidateInHex(file, "a6afd7bbb8d59e5bb12c9dbcc4ec2cff"));
        }

        [Fact]
        public static void Default_5_ValidateInAll_WithContent()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            Assert.True(md5.ValidateInAll("", "1B2M2Y8AsgTpgAmY7PhCfg=="));
            Assert.True(md5.ValidateInAll("123", "ICy5YqxZB1uWSwcVLSNLcA=="));
            Assert.True(md5.ValidateInAll("", "d41d8cd98f00b204e9800998ecf8427e"));
            Assert.True(md5.ValidateInAll("123", "202cb962ac59075b964b07152d234b70"));
        }

        [Fact]
        public static void Default_5_ValidateInAll_WithFile()
        {
            Core.Crypto.Md5.Register<Crypto.BouncyCastle.Md5>();
            var md5 = Core.Crypto.Md5.GetInstance();
            Assert.NotNull(md5);
            var path = @"%USERPROFILE%\.htc_test\TestData.Md5.txt";
            if (!Core.Runtime.Platform.IsWindows)
            {
                path = @"%HOME%/TestData.Md5.txt";
            }
            var file = new FileInfo(Environment.ExpandEnvironmentVariables(path));
            Assert.True(md5.ValidateInAll(file, "pq/Xu7jVnluxLJ28xOws/w=="));
            Assert.True(md5.ValidateInAll(file, "a6afd7bbb8d59e5bb12c9dbcc4ec2cff"));
        }
    }
}
