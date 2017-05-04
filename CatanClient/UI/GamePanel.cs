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
             if (backgroundHexagon==null)
             {
                 backgroundHexagon = new Hexagon(Panel.Width/2, Panel.Height/2, Panel.Height/2, new Pen(Color.FromArgb(104,116,68),0.5f), Resources.ForestBackground);
             }
             backgroundHexagon.Draw(e.Graphics);


            /*
              if (foregroundHexagones == null)
              {
                  foregroundHexagones = generateForegroundHexagones();
              }




              for (int i = 0; i < foregroundHexagones.GetLength(0); i++)
              {
                  for (int j = 0; j < foregroundHexagones[i].GetLength(0); j++)
                  {
                      foregroundHexagones[i][j].Draw(e.Graphics);
                  }
              }*/
        }
        /*
        private Hexagon[][] generateForegroundHexagones()
        {
            int foregroundHexagonItemHeightWidth = 100;
            var hexTest = CatanHexagonGenerator.GetCatanHexagoneGrid(backgroundHexagon.X + (backgroundHexagon.Width / 2) - (3 * (foregroundHexagonItemHeightWidth / 2)), backgroundHexagon.Y, foregroundHexagonItemHeightWidth, foregroundHexagonItemHeightWidth, new Pen(Color.Black, 25), null);
            return CatanHexagonGenerator.GetCatanHexagoneGrid(backgroundHexagon.X + (backgroundHexagon.Width / 2) - (3 * 50), backgroundHexagon.Y +
                (Math.Abs((backgroundHexagon.Points[0].Y - backgroundHexagon.Points[3].Y)) - Math.Abs(hexTest[0][1].Points[0].Y - hexTest[6][1].Points[3].Y)) / 2, foregroundHexagonItemHeightWidth, foregroundHexagonItemHeightWidth, new Pen(Color.Black, 25), null);
        }
        */
    }
}
