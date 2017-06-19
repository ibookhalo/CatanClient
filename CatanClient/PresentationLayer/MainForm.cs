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
using Catan.Game;

namespace Catan.Client.PresentationLayer
{
    partial class MainForm : Form, Interfaces.IPresentationLayer
    {
        private enum UIMode { LoadingDuringConnecting, WaitingForClientsAfterConnectingToServer, MainForm }

        private Interfaces.ILogicLayer_PresentationLayer iLogicLayer_PresentationLayer;
        private GamePanel gamePanel;
        private CatanClient currentClient;
        private List<CatanClient> catanClients;

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
        public void UpdateGame(GameStateMessage gameStateMessage)
        {
            executeUICodeThreadSafe(delegate
            {
                this.currentClient = gameStateMessage.CurrentClient;
                updatePlayerInfoControls();
            });
        }

        private void updatePlayerInfoControls()
        {
            // Den aktuellen Spieler markieren und die restlichen disablen ....
            foreach (var playerInfoControl in Controls.OfType<PlayerInformationControl>())
            {
                var catanClient = catanClients.Find(client => client.IPAddressPortNr.Equals(playerInfoControl.IPPort));

                if (playerInfoControl.IPPort.Equals(catanClient.IPAddressPortNr))
                {
                    playerInfoControl.IsSelected = true;
                    playerInfoControl.IsButtonStadtEnabled = existsTrueIn3DBoolArray(catanClient.AllowedStaedte);
                    playerInfoControl.IsButtonStrasseEnabled = existsTrueIn3DBoolArray(catanClient.AllowedStrassen);
                }
                playerInfoControl.IsSelected = false;
            }
        }
        private bool existsTrueIn3DBoolArray(bool[][][] array)
        {
            for (int i = 0; i < array?.GetLength(0); i++)
            {
                for (int j = 0; j < array[i].GetLength(0); j++)
                {
                    for (int k = 0; k < array[i][j].GetLength(0); k++)
                    {
                        if (array[i][j][k])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private PlayerInformationControl getPlayerInformationControlByClientIPPort(string ipPort)
        {
            foreach (var playerInfoControl in this.Controls.OfType<PlayerInformationControl>())
            {
                if (playerInfoControl.IPPort.Equals(ipPort))
                {
                    return playerInfoControl;
                }
            }
            return null;
        }
        public void WaitForClientsToConnectWithServer()
        {
            if (gamePanel == null)
            {
                setUIMode(UIMode.WaitingForClientsAfterConnectingToServer);
            }
        }

        public void InitGamePanel(Game.Hexagon[][] hexagonFields,List<CatanClient> clients)
        {
            executeUICodeThreadSafe(delegate
            {
                if (this.gamePanel == null)
                {
                    this.catanClients = clients;

                    this.Controls.Clear();
                    this.Controls.Add((this.gamePanel = new PresentationLayer.GamePanel(hexagonFields)).Panel);
                    int widthFactor = (int)(Width * 0.2f);
                    gamePanel.Panel.Location = new Point(widthFactor, 0);
                    gamePanel.Panel.Size = new Size(Width - (2 * widthFactor), Height);
                    this.BackColor = gamePanel.Panel.BackColor;

                    initPlayersInformationControls();
                    updatePlayerInfoControls();
                }
            });
        }
        private void initPlayersInformationControls()
        {
            int y_left_offset=1;
            int y_right_offset=1;

            int y_left = 1;
            int y_right = 1;

            for (int index = 0; index < catanClients.Count; index++)
            {
                var playerInfoControl = new PlayerInformationControl(catanClients[index]);

                if (index%2==0)
                {
                    playerInfoControl.Location = new Point(y_left_offset, y_left);
                    y_left = this.Height - playerInfoControl.Height-y_left;
                }
                else
                {
                    playerInfoControl.Location = new Point(this.Width-playerInfoControl.Width- y_right_offset, y_right);
                    y_right = this.Height - playerInfoControl.Height- y_right_offset;
                }
                this.Controls.Add(playerInfoControl);
            }
        }
    }
}
