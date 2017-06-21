using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Catan.Game;

namespace Catan.Client.PresentationLayer
{
    public partial class PlayerInformationControl : UserControl
    {
        public delegate void SiedlungBauenEventHandler(object ob, PlayerControlEventArg e);
        public event SiedlungBauenEventHandler SiedlungBauenClick;

        public delegate void TurnDoneEventHandler(object ob, PlayerControlEventArg e);
        public event TurnDoneEventHandler TurnDoneClick;


        private bool isButtonStrasseVisible;
        private bool isButtonSiedlungVisible;
        private bool isButtonTurnDoneVisible;
        private bool isWaiting;
        private bool isSelected;

        public bool IsSelected
        {
            set
            {
                this.BorderStyle = value ? BorderStyle.FixedSingle : BorderStyle.None;
                this.Enabled = value;
                btnTurn.Visible = value;
                btnSiedlungBauen.Visible = value;
                btnStrasseBauen.Visible = value;
                this.isSelected = value;
            }
            get
            {
                return this.isSelected;
            }
        }
        public bool IsWaiting
        {
            set
            {
                pbWait.Visible = value;
                this.isWaiting = value;
            }
            get
            {
                return this.isWaiting;
            }
        }
        public bool IsButtonStrasseVisible
        {
            get
            {
                return isButtonStrasseVisible;
            }

            set
            {
                this.btnStrasseBauen.Visible = value;
                isButtonStrasseVisible = value;
            }
        }
        public bool IsButtonSiedlungVisible
        {
            get
            {
                return isButtonSiedlungVisible;
            }

            set
            {
                this.btnSiedlungBauen.Visible = value;
                isButtonSiedlungVisible = value;
            }
        }
        public bool IsButtonTurnDoneVisible
        {
            get
            {
                return isButtonTurnDoneVisible;
            }

            set
            {
                this.btnTurn.Visible = value;
                isButtonTurnDoneVisible = value;
            }
        }

        public CatanClient CatanClient {get;private set;}

        public PlayerInformationControl(CatanClient client)
        {
            InitializeComponent();
            this.CatanClient = client;
            
            this.lblPlayerName.Text = client.Name;
            this.pnlColor.BackColor = client.Color;
            this.lblIPAddress.Text = client.IPAddressPortNr;

            this.IsWaiting = false;
            RefreshPunkte();
        }
        public void RefreshPunkte()
        {
            foreach (var rohstoffkarte in  Enum.GetValues(typeof(Game.KartenContainer.Rohstoffkarte)))
            {
                SetAntzahlKartenByLandfeldTyp((KartenContainer.Rohstoffkarte)rohstoffkarte, CatanClient.KartenContainer.GetAnzahlByRohstoffkarte((KartenContainer.Rohstoffkarte)rohstoffkarte));
            }
        }

        private void SetAntzahlKartenByLandfeldTyp(Catan.Game.KartenContainer.Rohstoffkarte rohstoff, int punkte)
        {
            switch (rohstoff)
            {
                case KartenContainer.Rohstoffkarte.Wolle: lblWolle.Text = $"{punkte}"; break;
                case KartenContainer.Rohstoffkarte.Getreide: lblGetreide.Text = $"{punkte}"; break;
                case KartenContainer.Rohstoffkarte.Gold: lblGold.Text = $"{punkte}"; break;
                case KartenContainer.Rohstoffkarte.Eisen: lblEisen.Text = $"{punkte}"; break;
                case KartenContainer.Rohstoffkarte.Wasser: lblWasser.Text = $"{punkte}"; break;
                case KartenContainer.Rohstoffkarte.Bewohner: lblBewohner.Text = $"{punkte}"; break;
                case KartenContainer.Rohstoffkarte.Holz: lblHolz.Text = $"{punkte}"; break;
                default:
                    throw new NotImplementedException($"SetAntzahlKartenByLandfeldTyp({rohstoff},  ...)");
            }
        }

        private void btnTurn_Click(object sender, EventArgs e)
        {
            TurnDoneClick?.Invoke(this, new PresentationLayer.PlayerControlEventArg(this.CatanClient));
        }

        private void btnSiedlungBauen_Click(object sender, EventArgs e)
        {
            SiedlungBauenClick?.Invoke(this,new PresentationLayer.PlayerControlEventArg(this.CatanClient));
        }
    }
}
