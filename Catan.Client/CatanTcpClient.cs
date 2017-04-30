using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Catan.Client
{
    public class CatanTcpClient
    {
        public TcpClient TcpClient { private set; get; }
        private IPAddress serverIpAddress;
        private const ushort SERVER_PORT = 123;

        public CatanTcpClient(IPAddress serverIpAddress)
        {
            this.TcpClient = new TcpClient();
            this.serverIpAddress = serverIpAddress;
        }

        public void ConnectAsync()
        {
            try
            {
                TcpClient.BeginConnect(serverIpAddress, SERVER_PORT, connectCallback, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void connectCallback(IAsyncResult ar)
        {
            TcpClient.EndConnect(ar);

        }
    }
}
