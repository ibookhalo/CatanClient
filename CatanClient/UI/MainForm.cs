using Catan.Client;
using Catan.Network.Messaging;
using Catan.Network.Messaging.ClientMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatanClient.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData==Keys.Escape)
            {
                Close();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(tbServerIPAddress.Text) || string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(tbNickname.Text))
            {
                MessageBoxHelper.ShowErrorMessage(new Exception("Bitte alle Felder ausfüllen !"));
            }
            else
            {
                IPAddress serverIp;
                if (IPAddress.TryParse(tbServerIPAddress.Text, out serverIp))
                {
                    try
                    {
                        connectToServer(serverIp);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxHelper.ShowErrorMessage(ex);
                    }
                }
                else
                {
                    MessageBoxHelper.ShowErrorMessage(new Exception("Bitte eine gültige Server-Adresse eingeben !"));
                }
            }
        }
        private void connectToServer(IPAddress serverIp)
        {
            try
            {
                CatanTcpClient catanClient = new CatanTcpClient(serverIp);
                catanClient.ConnectAsync();



                /*
                NetworkMessageWriter netMessageWriter = new NetworkMessageWriter(catanClient.TcpClient);
                netMessageWriter.WriteCompleted += NetMessageWriter_WriteCompleted;
                netMessageWriter.WriteError += NetMessageWriter_WriteError;
                netMessageWriter.WriteAsync(new CatanClientAuthenticationMessage(tbPassword.Text, tbNickname.Text));
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void NetMessageWriter_WriteError(object obj, Catan.Network.Events.NetworkMessageWriterWriteErrorEventArgs e)
        {
            MessageBoxHelper.ShowErrorMessage(e.Exception);
        }

        private void NetMessageWriter_WriteCompleted(object obj, Catan.Network.Events.NetworkMessageWriterWriteCompletedEventArgs e)
        {
            // Auth OK

        }
    }
}
