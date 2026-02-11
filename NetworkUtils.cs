using System.Linq;
using System.Net;
using System.Net.Sockets;


namespace MyNetworkScanner
{
    public static class NetworkUtils
    {
        public static string GetLocalIpBase()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());

                
                var ip = host.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

                // Ha nincs IP, fallback érték
                if (ip == null) return "192.168.1.";

                var ipString = ip.ToString();
                int lastDotIndex = ipString.LastIndexOf('.');

               
                return ipString.Substring(0, lastDotIndex + 1);
            }
            catch
            {
                return "192.168.1."; 
            }
        }
    }
}