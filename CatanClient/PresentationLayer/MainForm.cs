using Catan.Client;
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catan.Client.PresentationLayer
{
    partial class MainForm : Form, Interfaces.IPresentationLayer
    {
        private enum UIMode { LoadingDuringConnecting, WaitingForClientsAfterConnectingToServer, MainForm }

        private Interfaces.ILogicLayer_PresentationLayer iLogicLayer_PresentationLayer;
        private GamePanel gamePanel;

        public MainForm(Interfaces.ILogicLayer_PresentationLayer iLogicLayer_PresentationLayer)
        {
            InitializeComponent();
            this.iLogicLayer_PresentationLayer = iLogicLayer_PresentationLayer;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            tbNickname.Text = "test";
            tbPassword.Text = "ibo";
            tbServerIPAddress.Text = "127.0.0.1";
            if (string.IsNullOrWhiteSpace(tbServerIPAddress.Text) || string.IsNullOrWhiteSpace(tbPassword.Text) || string.IsNullOrWhiteSpace(tbNickname.Text))
            {
                MessageBoxHelper.ShowErrorMessage(new Exception("Bitte alle Felder ausfüllen !"));
            }
            else
            {
                IPAddress serverIp;
                if (IPAddress.TryParse(tbServerIPAddress.Text, out serverIp))
                {
                    iLogicLayer_PresentationLayer.ConnectToCatanServerAsync(serverIp, tbPassword.Text, tbNickname.Text);
                    setUIMode(UIMode.LoadingDuringConnecting);
                }
                else
                {
                    MessageBoxHelper.ShowErrorMessage(new Exception("Bitte eine gültige Server-Adresse eingeben !"));
                }
            }
        }
        private void setUIMode(PresentationLayer.MainForm.UIMode uiMode)
        {
            executeUICodeThreadSafe(delegate ()
            {
                if (uiMode == UIMode.LoadingDuringConnecting || uiMode == UIMode.MainForm)
                {
                    bool isLoading = uiMode == UIMode.LoadingDuringConnecting;
                    if (isLoading)
                        lblWaitingForClients.Visible = false;

                    pbLoading.Visible = isLoading;
                    btnConnect.Visible = !isLoading;
                    tbNickname.Enabled = tbPassword.Enabled = tbServerIPAddress.Enabled = !isLoading;
                    lblWaitingForClients.Visible = false;
                }
                else if (uiMode == UIMode.WaitingForClientsAfterConnectingToServer)
                {
                    pbLoading.Visible = true;

                    lblWaitingForClients.Visible = true;
                    lblWaitingForClients.Text = "Auf Clients warten ...";
                }
                else
                {
                    throw new NotImplementedException();
                }
            });
        }
        public void ThrowException(Exception ex)
        {
            if (gamePanel == null)
            {
                setUIMode(UIMode.MainForm);
            }
            MessageBoxHelper.ShowErrorMessage(ex);
        }
        private void executeUICodeThreadSafe(MethodInvoker methodInvoker)
        {
            this.Invoke(methodInvoker);
        }
        public void UpdateGame(GameStateMessage gameState)
        {
            executeUICodeThreadSafe(delegate
            {
                if (this.gamePanel == null)
                {
                    this.Controls.Clear();
                    this.Controls.Add((this.gamePanel = new PresentationLayer.GamePanel(gameState.HexagoneFields)).Panel);
                    int widthFactor = (int)(Width * 0.2f);
                    gamePanel.Panel.Location = new Point(widthFactor, 0);
                    gamePanel.Panel.Size = new Size(Width - (2 * widthFactor), Height);
                    var a = new Button() { Text = "Do Work" };
                    a.Click += A_Click;
                    this.Controls.Add(a);
                }
                //gamePanel.DrawPlayersInfo(gameState.Clients, gameState.CurrentClient);
            });
        }

        private void A_Click(object sender, EventArgs e)
        {
            iLogicLayer_PresentationLayer.OnClickBaueSiedlungen();
        }

        private void doActionAfterDelay(int secounds, MethodInvoker method)
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(secounds * 1000);
                method.Invoke();
            });
        }
        public void WaitForClientsToConnectWithServer()
        {
            if (gamePanel == null)
            {
                setUIMode(UIMode.WaitingForClientsAfterConnectingToServer);
            }
        }
    }
}
