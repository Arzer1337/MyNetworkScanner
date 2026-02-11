
using MyNetworkScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace MyNetworkScanner
{
    public class NetworkScannerService : INetworkScanner
    {
        private const int PingTimeoutMs = 500;
        private const int StartIpRange = 1;
        private const int EndIpRange = 254;

        public async Task<IEnumerable<NetworkDevice>> ScanSubnetAsync(string baseIp)
        {
            var tasks = new List<Task<NetworkDevice?>>();

            // Validálás
            if (!baseIp.EndsWith(".")) baseIp += ".";

            for (int i = StartIpRange; i <= EndIpRange; i++)
            {
                string currentIp = $"{baseIp}{i}";
                tasks.Add(CheckIpAddressAsync(currentIp));
            }

            // Párhuzamos futtatás 
            var results = await Task.WhenAll(tasks);

            // Null check és szűrés (LINQ)
            return results
                .Where(device => device != null)
                .Cast<NetworkDevice>() 
                .OrderBy(d => d.IpAddress.Length) 
                .ThenBy(d => d.IpAddress);
        }

       
        private async Task<NetworkDevice?> CheckIpAddressAsync(string ip)
        {
            using var ping = new Ping();

            try
            {
                var reply = await ping.SendPingAsync(ip, PingTimeoutMs);

                if (reply.Status == IPStatus.Success)
                {
                    return new NetworkDevice
                    {
                        IpAddress = ip,
                        LatencyMs = reply.RoundtripTime,
                        HostName = await GetHostNameAsync(ip)
                    };
                }
            }
            catch (PingException)
            {
                
            }

            return null; 
        }

       
        private async Task<string> GetHostNameAsync(string ip)
        {
            try
            {
                var hostEntry = await Dns.GetHostEntryAsync(ip);
                return hostEntry.HostName;
            }
            catch
            {
                return "Unknown Device";
            }
        }
    }
}