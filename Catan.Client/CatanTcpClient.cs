using Catan.Client.EventArgs;
using Catan.Network;
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Catan.Network.EventArgs;

namespace Catan.Client
{
    public class CatanTcpClient
    {
        public TcpClient TcpClient { private set; get; }
        private IPAddress serverIpAddress;
        private const ushort SERVER_PORT = 123;
        private const string AUTH_ERROR_EXCEPTION_MSG = "Fehler bei der Authentifizierung";

        public delegate void ClientConnectedToServerSuccessfullyHandler(object obj, System.EventArgs e);
        public delegate void ClientConnectedToServerErrorHandler(object obj, ClientConnectedToServerErrorEventArgs e);

        public event ClientConnectedToServerSuccessfullyHandler ClientConnectedToServerSuccessfully;
        public event ClientConnectedToServerErrorHandler ClientConnectedToServerError;

        public CatanTcpClient(IPAddress serverIpAddress)
        {
            this.TcpClient = new TcpClient();
            this.serverIpAddress = serverIpAddress;GameStateMessage a;
        }

        public void ConnectToCatanServerAsync(CatanClientAuthenticationMessage catanAuthMessage)
        {
            try
            {
                TcpClient.BeginConnect(serverIpAddress, SERVER_PORT, connectCallback, catanAuthMessage);
            }
            catch (Exception ex)
            {
                ClientConnectedToServerError?.Invoke(this, new ClientConnectedToServerErrorEventArgs(ex));
            }
        }

        private void connectCallback(IAsyncResult ar)
        {
            try
            {
                TcpClient.EndConnect(ar);

                NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(TcpClient);
                netMessageWriter.WriteCompleted += NetMessageWriter_CatanClientAuth_WriteCompleted;
                netMessageWriter.WriteError += (o, ee) => { ClientConnectedToServerError?.Invoke(this, new ClientConnectedToServerErrorEventArgs(ee.Exception)); };

                netMessageWriter.WriteAsync(ar.AsyncState as CatanClientAuthenticationMessage);
            }
            catch (Exception ex)
            {
                ClientConnectedToServerError?.Invoke(this, new EventArgs.ClientConnectedToServerErrorEventArgs(ex));
            }
            
        }
        private void NetMessageWriter_CatanClientAuth_WriteCompleted(object obj, NetworkMessageWriterWriteCompletedEventArgs e)
        {
            NetworkMessageReader netMessageReader = new NetworkMessageReader(TcpClient);
            netMessageReader.ReadCompleted += NetMessageReader_CatanClientAuth_ReadCompleted;
            netMessageReader.ReadError += (o, ee) => { ClientConnectedToServerError?.Invoke(this, new ClientConnectedToServerErrorEventArgs(new Exception(AUTH_ERROR_EXCEPTION_MSG, ee.Exception))); };

            netMessageReader.ReadAsync();
        }

        private void NetMessageReader_CatanClientAuth_ReadCompleted(object obj, NetworkMessageReaderReadCompletedEventArgs e)
        {
            if (e.NetworkMessage is CatanClientAuthenticationMessage)
            {
                ClientConnectedToServerSuccessfully?.Invoke(this, new System.EventArgs());
            }
            else
            {
                ClientConnectedToServerError?.Invoke(this, new ClientConnectedToServerErrorEventArgs(new Exception(AUTH_ERROR_EXCEPTION_MSG)));
            }
        }
    }
}
