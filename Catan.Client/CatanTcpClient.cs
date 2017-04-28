using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Catan.Client
{
    class CatanTcpClient
    {
        private TcpClient tcpClient;
        private IPAddress serverIpAddress;
        private const ushort SERVER_PORT = 123;

        public CatanTcpClient(IPAddress serverIpAddress)
        {
            this.tcpClient = new TcpClient();
            this.serverIpAddress = serverIpAddress;
        }

        public void Connect()
        {
            try
            {
                tcpClient.Connect(serverIpAddress, SERVER_PORT);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
