using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Network.Messaging;

namespace Catan.Client.Interfaces
{
    interface ILogicLayer_NetworkLayer
    {
        void ClientConnectToServerError(Exception ex);
        void ClientConnectedToServerSuccessfully();
        void ClientReceivedMessageCompleted(NetworkMessage networkMessage);
        void ClientReceivedMessageError(Exception exception);
    }
}
