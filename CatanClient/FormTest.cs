using CatanClient.Properties;
using CatanClient.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
            panelImages.Paint += Panel_Paint;
            this.FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            /*
            Panel p2 = new Panel();
            p2.Dock = DockStyle.Fill;
            //p2.BackColor = Color.Transparent;
            panelImages.Controls.Add(p2);
            p2.Paint += P2_Paint;
            */
        }

        private void P2_Paint(object sender, PaintEventArgs e)
        {
            int penWidth = 15;
            Hexagone[][] hexGrid = CatanHexagonGridGenerator.GetCatanHexagoneGrid(400, 0, 150, 150,new Pen(Color.Black, penWidth),Resources.test);

            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    
                    e.Graphics.FillPolygon(Brushes.Transparent, hexGrid[rowIndex][columnIndex].Points);
                    e.Graphics.DrawPolygon(new Pen(Color.Black, penWidth), hexGrid[rowIndex][columnIndex].Points);
                }
            }
        }
       
      
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            int hexW, hexH;
            
            hexH = panelImages.Height;
            hexW = hexH;

            int hexX, hexY;
            hexX = panelImages.Width/2 -(hexW/2);
            hexY = 0;
            Hexagone hex = new Hexagone(hexX,hexY,hexH,hexW, new Pen(Color.Black, 5), null);
            hex.Draw(e.Graphics);

            //  ;
            var hexTest = CatanHexagonGridGenerator.GetCatanHexagoneGrid(hexX + (hexW / 2) - (3 * 50), hexY, 100, 100, new Pen(Color.Black, 15), null);

            Hexagone[][] hexes = CatanHexagonGridGenerator.GetCatanHexagoneGrid(hexX+(hexW/2)-(3*50), hexY+ (Math.Abs((hex.Points[0].Y - hex.Points[3].Y)) - Math.Abs (hexTest[0][1].Points[0].Y - hexTest[6][1].Points[3].Y))/2, 100, 100, new Pen(Color.Black, 25), Resources.test);
          


            //hexes[3][0].Edges[5].Point1- hexes[3][0].Edges[5].Point1
            for (int i = 0; i < hexes.GetLength(0); i++)
            {
                for (int j = 0; j < hexes[i].GetLength(0); j++)
                {
                    hexes[i][j].Draw(e.Graphics);
                }
            }



        }
    }
}
