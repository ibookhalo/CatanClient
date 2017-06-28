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
using System.Net.Sockets;

namespace Catan.Client.LogicLayer
{
    class GameLogic : Interfaces.INetwork_LogicLayer, Interfaces.IPresentationLayer_LogicLayer
    {
        private Catan.Client.Interfaces.IPresentationLayer iPresentationLayer;
        private Catan.Client.Interfaces.INetworkLayer iNetworkLayer;
        private int myCientID;
        
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
                var gameStateMessage = networkMessage as GameStateMessage;
                CatanClient me = null;
                // überprüfen ob das Message für mich ist
                if ((me=gameStateMessage.Clients.Find(client => client.IPAddressPortNr.Equals(iNetworkLayer.GetLocalEndPoint().ToString())))!=null)
                {
                    if (myCientID==0)
                    {
                        myCientID = me.ID;
                        iPresentationLayer.InitGamePanel(gameStateMessage.HexagoneFields,gameStateMessage.Clients);
                        iPresentationLayer.UpdateGame(gameStateMessage);
                    }
                    else if (myCientID.Equals(me.ID))
                    {
                        iPresentationLayer.UpdateGame(gameStateMessage);
                    }
                    
                }
                /*
                else
                {
                    iNetworkLayer.Disconnect();
                }*/
            }
            else
            {
                // falsches Format?
                throw new NotImplementedException("Unknown Message Object");
            }
        }

        public void ClientReceivedMessageError(Exception exception)
        {
            iPresentationLayer.ThrowException(exception);
        }

        public EndPoint GetLocalEndPoint()
        {
            return iNetworkLayer.GetLocalEndPoint();
        }

        public int GetMyClientID()
        {
            return myCientID;
        }

        public void BaueSiedlung(SiedlungTexture siedlung)
        {
            var newSpielFiguren= new List<SpielFigur>();
            newSpielFiguren.Add(new Catan.Game.Siedlung(siedlung.HexagonPositionHexagonPoint.HexagonPosition, siedlung.HexagonPositionHexagonPoint.HexagonPoint));
            iNetworkLayer.SendMessage(new Network.Messaging.CatanClientStateChangeMessage(newSpielFiguren,null,null, myCientID, false));
        }

        public void BaueStrasse(StrasseTexture clickedStrasse)
        {
            throw new NotImplementedException();
        }
    }
}
