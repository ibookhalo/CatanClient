using CatanClient.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatanClient.UI
{
    class GamePanel
    {
        public Panel Panel { private set; get; }
        private Hexagon backgroundHexagon;
        private Hexagon[][] foregroundHexagones;
        public void DrawCircle(Graphics g, Pen pen,
                                 float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public GamePanel()
        {
            Panel = new Panel();
            Panel.Dock = DockStyle.Fill;
            Panel.BackColor = Color.White;
            
            Panel.Paint += Panel_Paint;
            Panel.MouseClick += Panel_MouseClick;
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"x: {e.X},    y:  {e.Y}");
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
             e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var pen = new Pen(Color.FromArgb(104, 116, 68), 0.5f);
             if (backgroundHexagon==null)
             {
                 backgroundHexagon = new Hexagon(Panel.Width/2, Panel.Height/2, Panel.Height/2, pen, null,true);
             }
             backgroundHexagon.Draw(e.Graphics);

            
            var testHex = new Hexagon(backgroundHexagon.X, backgroundHexagon.Y, backgroundHexagon.Radius*0.62f, pen, null, false);
            testHex.Draw(e.Graphics);

              float radius = (2 * testHex.Radius / 5) / 2;
            
            Hexagon hextest2 = new UI.Hexagon(testHex.Points[4].X, testHex.Points[4].Y, radius, pen, null, true);
             hextest2 = new UI.Hexagon(testHex.Points[4].X+ hextest2.Width/2, testHex.Points[4].Y, radius, pen, null, true);
            hextest2.Draw(e.Graphics);

            DrawCircle(e.Graphics, pen, testHex.X, testHex.Y, testHex.Radius);


            /*
              if (foregroundHexagones == null)
              {
                foregroundHexagones = CatanHexagonGenerator.GetCatanHexagoneGrid(testHex.Points[4].X, testHex.Points[4].Y, radius, pen, null);
              }




              for (int i = 0; i < foregroundHexagones.GetLength(0); i++)
              {
                  for (int j = 0; j < foregroundHexagones[i].GetLength(0); j++)
                  {
                      foregroundHexagones[i][j].Draw(e.Graphics);
                  }
              }*/
        }
    }
}
