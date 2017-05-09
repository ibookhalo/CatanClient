using CatanClient.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace CatanClient.UI
{
    class GamePanel
    {
        public TestPanel Panel { private set; get; }
        private Hexagon backgroundHexagon;
        private Hexagon[][] foregroundHexagones;
        private List<CatanCity> cities;
        bool lastWasIn = false;
        bool drawOK = false;
        public void DrawCircle(Graphics g, Pen pen,
                                 float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public GamePanel()
        {
            Panel = new TestPanel();
            Panel.Dock = DockStyle.Fill;
            Panel.BackColor = Color.White;
                
            Panel.Paint += Panel_Paint;
            Panel.MouseClick += Panel_MouseClick;

        }
        

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
           
            CatanCity remove = null;
            Region reg = null;
            foreach (var item in cities)
            {
                if ((reg = item.Region).IsVisible(e.X, e.Y))
                {
                    remove = item;
                    break;
                }
            }

          

            if (remove != null)
            {
                remove.Color = Color.Blue;
                Panel.Invalidate(reg);
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {

            var penBackgroundHex = new Pen(Color.Black, 10);

            if (backgroundHexagon == null)
            {
                backgroundHexagon = new Hexagon(Panel.Width / 2, Panel.Height / 2, Panel.Height / 2, penBackgroundHex, null, true);
            }
            backgroundHexagon.Draw(e.Graphics);
            drawOK = true;


            var penForegroundHex = new Pen(Color.Black, 5f);
            penForegroundHex.Alignment = PenAlignment.Inset;

            if (foregroundHexagones == null)
            {
                foregroundHexagones = CatanHexagonGenerator.GetCatanHexagoneGrid(backgroundHexagon.X, backgroundHexagon.Y, (backgroundHexagon.Radius /6), penForegroundHex);
            }
        

            for (int i = 0; i < foregroundHexagones.GetLength(0); i++)
            {
                for (int j = 0; j < foregroundHexagones[i].GetLength(0); j++)
                {

                    foregroundHexagones[i][j].Draw(e.Graphics);
                }
            }



            // ###############################
            var im = ImageHelper.ResizeImageAndTransparent(ImageHelper.ReplaceColor(Resources.HouseGray, Color.FromArgb(195, 195, 195), Color.Red), Color.White, 40, 40) as Bitmap;

            if (cities == null)
            {
                cities = new List<CatanCity>();
                for (int i = 0; i < foregroundHexagones.GetLength(0); i++)
                {
                    for (int j = 0; j < foregroundHexagones[i].GetLength(0); j++)
                    {
                        float x = foregroundHexagones[i][j].Points[3].X;
                        float y = foregroundHexagones[i][j].Points[3].Y;

                        CatanCity cc = new UI.CatanCity(x, y, im,Color.Red);
                        cities.Add(cc);
                    }
                }
            }
            cities.ForEach(c => c.Draw(e.Graphics));
        }
    }
}