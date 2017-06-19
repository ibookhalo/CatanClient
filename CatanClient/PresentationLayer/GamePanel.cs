using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Catan.Game;
using Catan.Client.Properties;

namespace Catan.Client.PresentationLayer
{
    class GamePanel
    {
        public  CustomPanel Panel { private set; get; }

        private Hexagon backgroundHexagon;
        private Hexagon[][] foregroundHexagones;
        private Game.Hexagon[][] hexagoneFields; // vom Server generiert !

        private Pen penBackgroundHex, penForegroundHex;

        public GamePanel(Game.Hexagon[][] hexagonFields)
        {
            this.hexagoneFields = hexagonFields;

            Panel = new PresentationLayer.CustomPanel();

            Panel.Paint += Panel_Paint;
            penBackgroundHex = new Pen(Color.Black, 10);
            penForegroundHex = new Pen(Color.FromArgb(240,209,164), 20f);
            penForegroundHex.Alignment = PenAlignment.Inset;
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            #region BackgroundHexagon

            if (backgroundHexagon == null)
            {
                backgroundHexagon = new Hexagon(Panel.Width / 2, Panel.Height / 2, 0, 0, Panel.Height / 2, penBackgroundHex, true);
            }
            backgroundHexagon.Draw(e.Graphics);

            #endregion

            #region ForegroundHexagones

            if (foregroundHexagones == null)
            {
                foregroundHexagones = CatanHexagonGenerator.GetCatanHexagoneGrid(backgroundHexagon.X, backgroundHexagon.Y, (backgroundHexagon.Radius / 6), penForegroundHex);
            }

            for (int rowIndex = 0; rowIndex < foregroundHexagones.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < foregroundHexagones[rowIndex].GetLength(0); columnIndex++)
                {
                    foregroundHexagones[rowIndex][columnIndex].BackgroundImage = getImageByLandfeldTyp(hexagoneFields[rowIndex][columnIndex].LandFeldTyp);
                    foregroundHexagones[rowIndex][columnIndex].Nr = hexagoneFields[rowIndex][columnIndex].Nr;
                    foregroundHexagones[rowIndex][columnIndex].Draw(e.Graphics);
                }
            }

            #endregion
        }
      
        private Image getImageByLandfeldTyp(Catan.Game.Hexagon.LandfeldTyp landfeldTyp)
        {
            switch (landfeldTyp)
            {
                case Catan.Game.Hexagon.LandfeldTyp.Weideland:
                    return Resources.BackgroundHexagonWeideland;
                case Catan.Game.Hexagon.LandfeldTyp.Ackerland:
                    return Resources.BackgroundHexagonAckerland;
                case Catan.Game.Hexagon.LandfeldTyp.BergwerkGold:
                    return Resources.BackgroundHexagonBerkwerkGold;
                case Catan.Game.Hexagon.LandfeldTyp.Eisenmine:
                    return Resources.BackgroundHexagonEisenmine;
                case Catan.Game.Hexagon.LandfeldTyp.MeersFeld:
                    return Resources.BackgroundHexagonMeeresfeld;
                case Catan.Game.Hexagon.LandfeldTyp.Wohnstaette:
                    return Resources.BackgroundHexagonWohnstaette;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
