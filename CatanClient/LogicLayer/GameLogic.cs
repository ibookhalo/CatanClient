using Catan.Client.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Catan.Network.Messaging;
using Catan.Game;

namespace Catan.Client.LogicLayer
{
    class GameLogic : Interfaces.ILogicLayer_NetworkLayer, Interfaces.ILogicLayer_PresentationLayer
    {
        private Catan.Client.Interfaces.IPresentationLayer iPresentationLayer;
        private Catan.Client.Interfaces.INetworkLayer iNetworkLayer;
        private int clientID;
        
        public GameLogic()
        {}
        public void StartGame()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((MainForm)(this.iPresentationLayer = new MainForm(this)));
        }
        public void ClientConnectToServerError(Exception ex)
        {
            this.iPresentationLayer.ThrowException(ex);
        }
        public void ClientConnectedToServerSuccessfully()
        {
            this.iPresentationLayer.WaitForClientsToConnectWithServer();
        }
        public void ConnectToCatanServerAsync(IPAddress serverIp,string authPass,string playerName)
        {
            this.iNetworkLayer = new NetworkLayer.CatanTcpClient(serverIp, this);
            this.iNetworkLayer.ConnectToCatanServerAsync(new Network.Messaging.CatanClientAuthenticationRequestMessage(authPass, playerName));
        }

        public void ClientReceivedMessageCompleted(NetworkMessage networkMessage)
        {
            if (networkMessage is Network.Messaging.GameStateMessage)
            {
                var gameState = networkMessage as GameStateMessage;
                CatanClient me = null;
                if ((me=gameState.Clients.Find(client => client.IPAddressPortNr.Equals(iNetworkLayer.GetLocalEndPoint().ToString())))!=null)
                {
                    if (clientID==0)
                    {
                        clientID = me.ID;
                    }
                    iPresentationLayer.UpdateGame(gameState);
                }
                else
                {
                    iNetworkLayer.Disconnect();
                }
            }
            else
            {

            }
        }

        public void ClientReceivedMessageError(Exception exception)
        {
            iPresentationLayer.ThrowException(exception);
        }

        public void OnClickBaueSiedlungen()
        {
            
        }
    }
}
