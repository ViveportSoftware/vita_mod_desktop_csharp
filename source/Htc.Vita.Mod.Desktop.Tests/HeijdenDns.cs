using System.Net;
using Htc.Vita.Mod.Desktop.HeijdenDns;
using Xunit;
using Xunit.Abstractions;

namespace Htc.Vita.Mod.Desktop.Tests
{
    public class HeijdenDns
    {
        private readonly ITestOutputHelper _output;

        public HeijdenDns(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public static void Default_0_GetInstance()
        {
            Core.Net.Dns.Register<DnsImpl>();
            var dns = Core.Net.Dns.GetInstance();
            Assert.NotNull(dns);
        }

        [Fact]
        public void Default_1_GetHostAddresses()
        {
            Core.Net.Dns.Register<DnsImpl>();
            var dns = Core.Net.Dns.GetInstance();
            Assert.NotNull(dns);
            var host = "www.google.com";
            var addresses = dns.GetHostAddresses(host);
            Assert.NotNull(addresses);
            Assert.NotEmpty(addresses);
            foreach (var address in addresses)
            {
                _output.WriteLine("address for \"" + host + "\": " + address);
            }
            var host2 = "172.217.27.132";
            var addresses2 = dns.GetHostAddresses(host2);
            Assert.NotNull(addresses2);
            Assert.NotEmpty(addresses2);
            var hasSameAddress = false;
            foreach (var address2 in addresses2)
            {
                _output.WriteLine("address for \"" + host2 + "\": " + address2);
                if (host2.Equals(address2.ToString()))
                {
                    hasSameAddress = true;
                }
            }
            Assert.True(hasSameAddress);
        }

        [Fact]
        public void Default_2_GetHostEntry()
        {
            Core.Net.Dns.Register<DnsImpl>();
            var dns = Core.Net.Dns.GetInstance();
            Assert.NotNull(dns);
            var host = "8.8.8.8";
            var entry = dns.GetHostEntry(host);
            Assert.NotNull(entry);
            _output.WriteLine("entry for \"" + host + "\": " + entry.HostName);
            var host2 = "www.google.com";
            var entry2 = dns.GetHostEntry(host2);
            Assert.NotNull(entry2);
            Assert.True(host2.Equals(entry2.HostName) || (host2 + ".").Equals(entry2.HostName));
        }

        [Fact]
        public void Default_2_GetHostEntry_WithIPAddress()
        {
            Core.Net.Dns.Register<DnsImpl>();
            var dns = Core.Net.Dns.GetInstance();
            Assert.NotNull(dns);
            var host = IPAddress.Parse("8.8.8.8");
            var entry = dns.GetHostEntry(host);
            Assert.NotNull(entry);
            _output.WriteLine("entry for \"" + host + "\": " + entry.HostName);
        }
    }
}
