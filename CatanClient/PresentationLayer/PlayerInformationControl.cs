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
        private CatanClient client;
        public string IPPort { get; private set; }
        private bool isButtonStrasseEnabled;
        private bool isButtonStadtEnabled;
        private bool isSelected;

        public bool IsSelected
        {
            set
            {
                this.BorderStyle = value ? BorderStyle.FixedSingle : BorderStyle.None;
                this.Enabled = value;
                btnTurn.Visible = value;
                btnStadtBauen.Visible = value;
                btnStrasseBauen.Visible = value;
                this.isSelected = value;
            }
            get
            {
                return this.isSelected;
            }
        }
        public bool IsButtonStrasseEnabled
        {
            get
            {
                return isButtonStrasseEnabled;
            }

            set
            {
                this.btnStrasseBauen.Enabled = value;
                isButtonStrasseEnabled = value;
            }
        }
        public bool IsButtonStadtEnabled
        {
            get
            {
                return isButtonStadtEnabled;
            }

            set
            {
                this.btnStadtBauen.Enabled = value;
                isButtonStadtEnabled = value;
            }
        }


        public PlayerInformationControl(CatanClient client)
        {
            InitializeComponent();
            this.client = client;

            IPPort = client.IPAddressPortNr;

            this.lblPlayerName.Text = client.Name;
            this.lblIPAddress.Text = IPPort;
            this.pnlColor.BackColor = client.Color;

            RefreshPunkte();
        }
        public void RefreshPunkte()
        {
            foreach (var rohstoffkarte in  Enum.GetValues(typeof(Game.KartenContainer.Rohstoffkarte)))
            {
                SetAntzahlKartenByLandfeldTyp((KartenContainer.Rohstoffkarte)rohstoffkarte, client.KartenContainer.GetAnzahlByRohstoffkarte((KartenContainer.Rohstoffkarte)rohstoffkarte));
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
    }
}
