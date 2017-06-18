using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.Interfaces
{
    interface INetworkLayer
    {
        void ConnectToCatanServerAsync(CatanClientAuthenticationRequestMessage catanAuthMessage);
        void Disconnect();
        EndPoint GetLocalEndPoint();
    }
}
