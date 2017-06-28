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

        private Interfaces.IPresentationLayer_LogicLayer iPresentationLayer_LogicLayer;
        private GamePanel gamePanel;
        private CatanClient currentClient;
        private List<CatanClient> catanClients;
        private List<PlayerInformationControl> clientInformationControls;

        public MainForm(Interfaces.IPresentationLayer_LogicLayer iLogicLayer_PresentationLayer)
        {
            InitializeComponent();
            this.iPresentationLayer_LogicLayer = iLogicLayer_PresentationLayer;
            this.clientInformationControls = new List<PresentationLayer.PlayerInformationControl>();
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
                    iPresentationLayer_LogicLayer.ConnectToCatanServerAsync(serverIp, tbPassword.Text, tbNickname.Text);
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
            this.catanClients = gameStateMessage.Clients;
            this.currentClient = gameStateMessage.CurrentClient;

            executeUICodeThreadSafe(delegate
            {
                updatePlayerInfoControls();
                gamePanel.DrawSpielFiguren(gameStateMessage.Clients);
            });
        }
        
        private void updatePlayerInfoControls()
        {
            // Den aktuellen Spieler markieren und die restlichen disablen ....
            foreach (var clientInfoControl in this.clientInformationControls)
            {
                clientInfoControl.RefreshClientData(this.catanClients.Find(client => client.ID == clientInfoControl.CatanClient.ID));


                clientInfoControl.IsSelected = clientInfoControl.CatanClient.ID.Equals(currentClient.ID);

                // me ?
                if (clientInfoControl.CatanClient.ID.Equals(iPresentationLayer_LogicLayer.GetMyClientID()) &&
                    currentClient.ID.Equals(iPresentationLayer_LogicLayer.GetMyClientID()))
                {
                    
                    clientInfoControl.IsButtonSiedlungVisible = existsTrueIn3DBoolArray(clientInfoControl.CatanClient.AllowedSiedlungen);
                    clientInfoControl.IsButtonStrasseVisible = existsTrueIn3DBoolArray(clientInfoControl.CatanClient.AllowedStrassen);
                    clientInfoControl.IsButtonTurnDoneVisible = true;
                }
                else
                {
                    clientInfoControl.IsButtonTurnDoneVisible=clientInfoControl.IsButtonSiedlungVisible = clientInfoControl.IsButtonStrasseVisible = false;
                    clientInfoControl.IsWaiting = clientInfoControl.CatanClient.ID.Equals(currentClient.ID);
                }
                clientInfoControl.RefreshPunkte();
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
                if (playerInfoControl.CatanClient.IPAddressPortNr.Equals(ipPort))
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
                    this.gamePanel.SiedlungClick += GamePanel_SiedlungClick;
                    this.gamePanel.StrasseClick += GamePanel_StrasseClick;

                    int widthFactor = (int)(Width * 0.2f);
                    gamePanel.Panel.Location = new Point(widthFactor, 0);
                    gamePanel.Panel.Size = new Size(Width - (2 * widthFactor), Height);
                    this.BackColor = gamePanel.Panel.BackColor;

                    initPlayersInformationControls();
                }
            });
        }

        private void GamePanel_StrasseClick(object ob, StrasseEventArgs e)
        {
            this.iPresentationLayer_LogicLayer.BaueStrasse(e.ClickedStrasse);
        }
        private void GamePanel_SiedlungClick(object ob, SiedlungEventArgs e)
        {
            this.iPresentationLayer_LogicLayer.BaueSiedlung(e.ClickedSiedlung);
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
                this.clientInformationControls.Add(playerInfoControl);

                if (catanClients[index].ID.Equals(iPresentationLayer_LogicLayer.GetMyClientID()))
                {
                    playerInfoControl.SiedlungBauenClick += PlayerInfoControl_OnClickSiedlungbauen;
                    playerInfoControl.StrasseBauenClick += PlayerInfoControl_StrasseBauenClick;
                    playerInfoControl.TurnDoneClick += PlayerInfoControl_OnClickTurndone;
                }
            }
        }

        private void PlayerInfoControl_StrasseBauenClick(object ob, PlayerControlEventArg e)
        {
            gamePanel.DrawPrototypStrassen(e.Client.AllowedStrassen);
        }

        private void PlayerInfoControl_OnClickTurndone(object ob, PlayerControlEventArg e)
        {
            
        }

        private void PlayerInfoControl_OnClickSiedlungbauen(object ob, PlayerControlEventArg e)
        {
            gamePanel.DrawPrototypSiedlungen(e.Client.AllowedSiedlungen);
        }
    }
}
