using Catan.Client;
using Catan.Network.Messaging;
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
                   connectToServerAsync(serverIp);
                }
                else
                {
                    MessageBoxHelper.ShowErrorMessage(new Exception("Bitte eine gültige Server-Adresse eingeben !"));
                }
            }
        }
        private void connectToServerAsync(IPAddress serverIp)
        {
            try
            {
                setUIMode(true);

                CatanTcpClient catanClient = new CatanTcpClient(serverIp);
                catanClient.ClientConnectedToServerSuccessfully += CatanClient_ClientConnectedToServerSuccessfully;
                catanClient.ClientConnectedToServerError += CatanClient_ClientConnectedToServerError;
                catanClient.ConnectToCatanServerAsync(new CatanClientAuthenticationMessage(tbPassword.Text,tbNickname.Text));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setUIMode(bool isLoading)
        {
            pbLoading.Visible = isLoading;
            btnConnect.Visible = !isLoading;
            tbNickname.Enabled = tbPassword.Enabled = tbServerIPAddress.Enabled = !isLoading;
        }
        private void CatanClient_ClientConnectedToServerError(object obj, Catan.Client.EventArgs.ClientConnectedToServerErrorEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                setUIMode(false);
            });
            MessageBoxHelper.ShowErrorMessage(e.Exception);
        }

        private void CatanClient_ClientConnectedToServerSuccessfully(object obj, EventArgs e)
        {
            // initGame
            Invoke((MethodInvoker)delegate
            {
                Game game = new UI.Game(this);
                game.Run();
            });
        }

        private void NetMessageWriter_WriteError(object obj, Catan.Network.EventArgs.NetworkMessageWriterWriteErrorEventArgs e)
        {
            MessageBoxHelper.ShowErrorMessage(e.Exception);
        }

        private void NetMessageWriter_WriteCompleted(object obj, Catan.Network.EventArgs.NetworkMessageWriterWriteCompletedEventArgs e)
        {
            // Auth OK

        }
    }
}
