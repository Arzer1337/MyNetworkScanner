using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace MyNetworkScanner
{
    public interface INetworkScanner
    {
        
        Task<IEnumerable<NetworkDevice>> ScanSubnetAsync(string baseIp);
    }
}
