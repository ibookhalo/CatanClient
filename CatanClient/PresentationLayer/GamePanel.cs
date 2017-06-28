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
        public delegate void SiedlungEventHandler(object ob,SiedlungEventArgs e);
        public event SiedlungEventHandler SiedlungGebautClick;

        public  CustomPanel Panel { private set; get; }

        private HexagonTexture backgroundHexagon;
        private HexagonTexture[][] foregroundHexagones;
        private Game.Hexagon[][] hexagoneFields; // vom Server generiert !
        private List<SiedlungTexture> protoTypSiedlungen;

        private Pen penBackgroundHex, penForegroundHex;

        public GamePanel(Game.Hexagon[][] hexagonFields)
        {
            this.hexagoneFields = hexagonFields;
            this.protoTypSiedlungen = new List<PresentationLayer.SiedlungTexture>();

            Panel = new PresentationLayer.CustomPanel();

            Panel.Paint += Panel_Paint;
            Panel.MouseClick += Panel_MouseClick;
            penBackgroundHex = new Pen(Color.Black, 10);
            penForegroundHex = new Pen(Color.FromArgb(240,209,164), 20f);
            penForegroundHex.Alignment = PenAlignment.Inset;
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            var foundSiedlung = protoTypSiedlungen.Find(siedlung => siedlung.Region.IsVisible(e.X, e.Y));
            if (foundSiedlung!=null)
            {
                // Transform from Client hexPoint to Server hexPoint
                foundSiedlung = new SiedlungTexture(new HexagonPositionHexagonPoint(foundSiedlung.HexagonPositionHexagonPoint.HexagonPosition,new HexagonPoint(getServerHexagonPointIndexByClientHexagonPointIndex(foundSiedlung.HexagonPositionHexagonPoint.HexagonPoint.Index))),
                    foundSiedlung.X, foundSiedlung.Y, foundSiedlung.Height, foundSiedlung.Width, foundSiedlung.Pen);

                SiedlungGebautClick?.Invoke(this, new PresentationLayer.SiedlungEventArgs(foundSiedlung));
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            #region BackgroundHexagon

            if (backgroundHexagon == null)
            {
                backgroundHexagon = new HexagonTexture(Panel.Width / 2, Panel.Height / 2, 0, 0, Panel.Height / 2, penBackgroundHex, true);
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
                    foregroundHexagones[rowIndex][columnIndex].BackgroundImage = ImageLoader.GetImageByLandfeldTyp(hexagoneFields[rowIndex][columnIndex].LandFeldTyp);
                    foregroundHexagones[rowIndex][columnIndex].Nr = hexagoneFields[rowIndex][columnIndex].Nr;
                    foregroundHexagones[rowIndex][columnIndex].Draw(e.Graphics);
                }
            }

            #endregion

            #region PrototypSiedlungen

            protoTypSiedlungen.ForEach(siedlung => siedlung.Draw(e.Graphics));
            
            #endregion
        }
        private int getClientHexagonPointIndexByServerHexagonPointIndex(int serverHexagonPointIndex)
        {
            switch (serverHexagonPointIndex)
            {
                case 0: return 3;
                case 1: return 2;
                case 2: return 1;
                case 3: return 0;
                case 4: return 5;
                case 5: return 4;
                default:
                    throw new NotImplementedException("getClientHexagonPointIndexByServerHexagonPointIndex no match serverHexagonPointIndex");
            }
        }
        private int getServerHexagonPointIndexByClientHexagonPointIndex(int clientHexagonPointIndex)
        {
            switch (clientHexagonPointIndex)
            {
                case 3: return 0;
                case 2: return 1;
                case 1: return 2;
                case 0: return 3;
                case 5: return 4;
                case 4: return 5;
                default:
                    throw new NotImplementedException("getServerHexagonPointIndexByClientHexagonPointIndex no match serverHexagonPointIndex");
            }
        }

        public void DrawPrototypSiedlungen(bool[][][] siedlungen)
        {
            this.protoTypSiedlungen.Clear();

            for (int hexagonRowIndex = 0; hexagonRowIndex < siedlungen.GetLength(0); hexagonRowIndex++)
            {
                for (int hexagonColumnIndex = 0; hexagonColumnIndex < siedlungen[hexagonRowIndex].GetLength(0); hexagonColumnIndex++)
                {
                    for (int hexagonPointIndex = 0; hexagonPointIndex < siedlungen[hexagonRowIndex][hexagonColumnIndex].GetLength(0); hexagonPointIndex++)
                    {
                        if (siedlungen[hexagonRowIndex][hexagonColumnIndex][hexagonPointIndex])
                        {
                            int hexPointIndex = 0;
                            var hexPoint = this.foregroundHexagones[hexagonRowIndex][hexagonColumnIndex].Points[hexPointIndex=getClientHexagonPointIndexByServerHexagonPointIndex(hexagonPointIndex)];
                            SiedlungTexture siedlung = new SiedlungTexture(new HexagonPositionHexagonPoint(new HexagonPosition(hexagonRowIndex,hexagonColumnIndex),new HexagonPoint(hexPointIndex)),
                                hexPoint.X, hexPoint.Y, 40, 40,new Pen(Color.Gray));

                            this.protoTypSiedlungen.Add(siedlung);

                            Panel.Invalidate(siedlung.Region);
                        }
                    }
                }
            }
        }
    }
}
