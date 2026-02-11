
using MyNetworkScanner;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetworkScannerPro
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            Console.WriteLine("===MyNetworkScanner===");
            Console.WriteLine("Ki a franc lopja a wifit? : ");
            Console.WriteLine("");

            // Automatikus IP detektálás
            string baseIp = NetworkUtils.GetLocalIpBase();
            Console.WriteLine($"Érzékelt hálózat: {baseIp}0/24");

            // Dependency Injection
            INetworkScanner scanner = new NetworkScannerService();

            // Mérés indítása
            var stopwatch = Stopwatch.StartNew();
            Console.Write("Szkennelés folyamatban... ");

          
            var activeDevices = await scanner.ScanSubnetAsync(baseIp);

            stopwatch.Stop();
            Console.WriteLine($"Kész! ({stopwatch.Elapsed.TotalSeconds:F2} másodperc)\n");

            // Eredmények megjelenítése
            PrintResults(activeDevices);

            Console.WriteLine("\nNyomj egy gombot a kilépéshez...");
            Console.ReadKey();
        }

        private static void PrintResults(System.Collections.Generic.IEnumerable<NetworkDevice> devices)
        {
            Console.WriteLine($"{"IP Cím",-16} | {"Ping",-6} | {"Eszköz Neve"}");
            Console.WriteLine(new string('-', 50));

            int count = 0;
            foreach (var device in devices)
            {
                
                Console.WriteLine(device.ToString());
                count++;
            }

            if (count == 0)
            {
                Console.WriteLine("Nem találtam aktív eszközt.");
            }
        }
    }
}