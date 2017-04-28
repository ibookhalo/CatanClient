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

namespace CatanClient
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
                MessageBox.Show("Bitte alle Felder ausfüllen !", "Felder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
              
                IPAddress serverIp;
                if (IPAddress.TryParse(tbServerIPAddress.Text, out serverIp))
                {

                }
                else
                {
                    MessageBox.Show("Bitte eine gültige Server-Adresse eingeben !", "Ungültige Adresse", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
