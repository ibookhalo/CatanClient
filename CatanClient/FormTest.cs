using CatanClient.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatanClient
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
            panel.Paint += Panel_Paint;
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            int penWidth =10;
            Hexagone hex = new Hexagone(0, 0, 200, 200, penWidth);


            e.Graphics.DrawPolygon(new Pen(Color.Red, penWidth),hex.Points);
        }
    }
}
