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
            WindowState = FormWindowState.Maximized;
            gamePanel.Paint += GamePanel_Paint;
        }
       
      

        private Point[] getHex(int hexHeight,int hexWidth,int x, int y,int penWidth)
        {
            int top = (int)(hexHeight * 0.25f);
            y += penWidth/2;
            x += penWidth/2;

            Point a = new Point((x+hexWidth/2),y);
            Point b = new Point(a.X+(hexWidth/2),a.Y+ top);
            Point c = new Point(b.X, b.Y+ (int)(hexHeight * 0.5f));
            Point d = new Point(a.X,c.Y+(top));
            Point e = new Point(x,c.Y);
            Point f = new Point(e.X,b.Y);

            return new Point[] { a, b,c,d,e,f};
        }
        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            int penWidth =20;
            e.Graphics.DrawPolygon(new Pen(Color.Red, penWidth), getHex(157,150,90,0,penWidth));
            e.Graphics.DrawPolygon(new Pen(Color.Red, penWidth), getHex(157, 150, 90+150, 0, penWidth));
            e.Graphics.DrawPolygon(new Pen(Color.Red, penWidth), getHex(157, 150, 90 + 150+150, 0, penWidth));
        }
       
    }
}
