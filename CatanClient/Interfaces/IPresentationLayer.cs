using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Network.Messaging;

namespace Catan.Client.Interfaces
{
    interface IPresentationLayer
    {
        void ThrowException(Exception ex);
        void UpdateGame(GameStateMessage gameState);
        void WaitForClientsToConnectWithServer();
    }
}
