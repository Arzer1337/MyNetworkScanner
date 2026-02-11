using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyNetworkScanner
{
    public class NetworkDevice
    {
        public string IpAddress { get; set; } = string.Empty;
        public string HostName { get; set; } = "Unknown";
        public long LatencyMs { get; set; }
        public MacAddressType DeviceType { get; set; } = MacAddressType.Generic; 

        public override string ToString()
        {
            return $"{IpAddress.PadRight(15)} | {LatencyMs.ToString().PadRight(4)} ms | {HostName}";
        }
    }

    public enum MacAddressType { Generic, Router, PC, Mobile }
}