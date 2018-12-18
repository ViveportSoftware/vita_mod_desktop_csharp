using System;
using System.Collections.Generic;
using System.Net;
using Heijden.DNS;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Mod.Desktop.HeijdenDns
{
    public class DnsImpl : Core.Net.Dns
    {
        private readonly Resolver _resolver;

        public DnsImpl(string resolver) : base(resolver)
        {
            List<IPEndPoint> endPoints = new List<IPEndPoint>();
            try
            {
                endPoints.Add(new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53)); // Google Public DNS
                endPoints.Add(new IPEndPoint(IPAddress.Parse("114.114.114.114"), 53)); // 114 DNS
                if (!string.IsNullOrWhiteSpace(resolver))
                {
                    endPoints.Add(new IPEndPoint(IPAddress.Parse(resolver), 53));
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DnsImpl)).Warn("[DnsImpl] Creating dns resolver error: " + e);
            }
            _resolver = new Resolver
            {
                Recursion = true,
                UseCache = true,
                DnsServers = endPoints.ToArray()
            };
        }

        protected override bool OnFlushCache()
        {
            _resolver.ClearCache();
            return true;
        }

        protected override bool OnFlushCache(string hostName)
        {
            return false;
        }

        protected override IPAddress[] OnGetHostAddresses(string hostNameOrAddress)
        {
            return _resolver.GetHostAddresses(hostNameOrAddress);
        }

        protected override IPHostEntry OnGetHostEntry(IPAddress ipAddress)
        {
            return _resolver.GetHostEntry(ipAddress);
        }

        protected override IPHostEntry OnGetHostEntry(string hostNameOrAddress)
        {
            return _resolver.GetHostEntry(hostNameOrAddress);
        }
    }
}
