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

        public PlayerInformationControl(CatanClient client)
        {
            InitializeComponent();
            this.client = client;

            this.lblPlayerName.Text = client.Name;
            this.lblIPAddress.Text = client.IPAddressPortNr;
        }
    }
}
