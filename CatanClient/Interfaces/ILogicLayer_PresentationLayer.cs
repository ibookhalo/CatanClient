using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.Interfaces
{
    interface ILogicLayer_PresentationLayer
    {
        void ConnectToCatanServerAsync(IPAddress serverIp, string authPass, string playerName);
        void OnClickBaueSiedlungen();
    }
}
