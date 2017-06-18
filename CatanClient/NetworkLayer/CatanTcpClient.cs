using Catan.Network;
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Catan.Network.EventArgs;
using Catan.Client.Interfaces;

namespace Catan.Client.NetworkLayer
{
    class CatanTcpClient:INetworkLayer
    {
        private ILogicLayer_NetworkLayer iLogicLayer_NetworkLayer;
        public TcpClient TcpClient { private set; get; }
        private IPAddress serverIpAddress;
        private const ushort SERVER_PORT = 123;
        private const string AUTH_ERROR_EXCEPTION_MSG = "Fehler bei der Authentifizierung";


        public CatanTcpClient(IPAddress serverIpAddress, ILogicLayer_NetworkLayer iLogicLayer_NetworkLayer)
        {
            this.iLogicLayer_NetworkLayer = iLogicLayer_NetworkLayer;
            this.TcpClient = new TcpClient();
            this.serverIpAddress = serverIpAddress;
        }
        
        public void ConnectToCatanServerAsync(CatanClientAuthenticationRequestMessage catanAuthMessage)
        {
            try
            {
                TcpClient.BeginConnect(serverIpAddress, SERVER_PORT, connectCallback, catanAuthMessage);
            }
            catch (Exception ex)
            {
                iLogicLayer_NetworkLayer.ClientConnectToServerError(ex);
            }
        }
        private void connectCallback(IAsyncResult ar)
        {
            try
            {
                TcpClient.EndConnect(ar);

                NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(TcpClient);
                netMessageWriter.WriteCompleted += NetMessageWriter_CatanClientAuth_WriteCompleted;
                netMessageWriter.WriteError += (o, ee) => { iLogicLayer_NetworkLayer.ClientConnectToServerError(ee.Exception); };

                netMessageWriter.WriteAsync(ar.AsyncState as CatanClientAuthenticationRequestMessage);
            }
            catch (Exception ex)
            {
                iLogicLayer_NetworkLayer.ClientConnectToServerError(ex);
            }
            
        }
        private void NetMessageWriter_CatanClientAuth_WriteCompleted(object obj, NetworkMessageWriterWriteCompletedEventArgs e)
        {
            NetworkMessageReader netMessageReader = new NetworkMessageReader(TcpClient);
            netMessageReader.ReadCompleted += NetMessageReader_CatanClientAuth_ReadCompleted;
            netMessageReader.ReadError += (o, ee) => { iLogicLayer_NetworkLayer.ClientConnectToServerError(new Exception(AUTH_ERROR_EXCEPTION_MSG, ee.Exception)); };

            netMessageReader.ReadAsync();
        }
        private void NetMessageReader_CatanClientAuth_ReadCompleted(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            if (e.NetworkMessage is CatanClientAuthenticationResponseMessage)
            {
                iLogicLayer_NetworkLayer.ClientConnectedToServerSuccessfully();

                var netReader=new NetworkMessageReader(TcpClient);
                netReader.ReadCompleted += NetworMessageReader_OnReceiveMessageCompleted;
                netReader.ReadError += NetworMessageReader_OnReceiveMessageError;

                netReader.ReadAsync(true);
            }
            else
            {
                iLogicLayer_NetworkLayer.ClientConnectToServerError(new Exception(AUTH_ERROR_EXCEPTION_MSG));
            }
        }
        private void NetworMessageReader_OnReceiveMessageError(object obj, NetworkMessageReaderReadErrorEventArgs e)
        {
            iLogicLayer_NetworkLayer.ClientReceivedMessageError(e.Exception);
        }
        private void NetworMessageReader_OnReceiveMessageCompleted(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            iLogicLayer_NetworkLayer.ClientReceivedMessageCompleted(e.NetworkMessage);
        }
        public EndPoint GetLocalEndPoint()
        {
            return TcpClient.Client.LocalEndPoint;
        }

        public void Disconnect()
        {
            TcpClient?.Close();
        }
    }
}
