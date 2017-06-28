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
        public event SiedlungEventHandler SiedlungClick;

        public delegate void StrasseEventHandler(object ob, StrasseEventArgs e);
        public event StrasseEventHandler StrasseClick;


        public  CustomPanel Panel { private set; get; }

        private HexagonTexture backgroundHexagon;
        private HexagonTexture[][] foregroundHexagones;
        private Game.Hexagon[][] hexagoneFields; // vom Server generiert !

        private List<SiedlungTexture> protoTypSiedlungen;
        private List<StrasseTexture> protoTypStrassen;

        private List<CatanClient> catanClients;

        private Pen penBackgroundHex, penForegroundHex;

        private bool shouldDrawProtoSiedlungen;
        private bool shouldDrawProtoStrassen;

        public GamePanel(Game.Hexagon[][] hexagonFields)
        {
            this.hexagoneFields = hexagonFields;

            this.protoTypSiedlungen = new List<PresentationLayer.SiedlungTexture>();
            this.protoTypStrassen = new List<PresentationLayer.StrasseTexture>();

            this.catanClients = new List<CatanClient>();

            Panel = new PresentationLayer.CustomPanel();

            Panel.Paint += Panel_Paint;
            Panel.MouseClick += Panel_MouseClick;
            penBackgroundHex = new Pen(Color.Black, 10);
            penForegroundHex = new Pen(Color.FromArgb(240,209,164), 20f);
            penForegroundHex.Alignment = PenAlignment.Inset;
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            #region Siedlung Click?

            var foundSiedlung = protoTypSiedlungen.Find(siedlung => siedlung.Region.IsVisible(e.X, e.Y));
            if (foundSiedlung != null)
            {
                // Transform from Client hexPoint to Server hexPoint
                foundSiedlung = new SiedlungTexture(new HexagonPositionHexagonPoint(foundSiedlung.HexagonPositionHexagonPoint.HexagonPosition, new HexagonPoint(getServerHexagonPointIndexByClientHexagonPointIndex(foundSiedlung.HexagonPositionHexagonPoint.HexagonPoint.Index))),
                    foundSiedlung.X, foundSiedlung.Y, foundSiedlung.Height, foundSiedlung.Width, foundSiedlung.Pen);

                SiedlungClick?.Invoke(this, new PresentationLayer.SiedlungEventArgs(foundSiedlung));
            }

            #endregion

            #region Strasse Click?



            #endregion
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

            if (shouldDrawProtoSiedlungen)
            {
                protoTypSiedlungen.ForEach(siedlung => siedlung.Draw(e.Graphics));
                shouldDrawProtoSiedlungen = false;
            }

            #endregion

            #region ProtoStrassen



            #endregion

            #region SpielFiguren

            foreach (var client in this.catanClients)
            {
                foreach (var siedlung in client.SpielfigurenContainer?.Siedlungen)
                {
                    var siedlungTexture = getSiedlungTextureByServerSiedlung(siedlung, client.Color);
                    siedlungTexture.Draw(e.Graphics);
                }
            }

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
            if (this.protoTypSiedlungen.Count <= 0)
            {
                for (int hexagonRowIndex = 0; hexagonRowIndex < siedlungen.GetLength(0); hexagonRowIndex++)
                {
                    for (int hexagonColumnIndex = 0; hexagonColumnIndex < siedlungen[hexagonRowIndex].GetLength(0); hexagonColumnIndex++)
                    {
                        for (int hexagonPointIndex = 0; hexagonPointIndex < siedlungen[hexagonRowIndex][hexagonColumnIndex].GetLength(0); hexagonPointIndex++)
                        {
                            if (siedlungen[hexagonRowIndex][hexagonColumnIndex][hexagonPointIndex])
                            {

                                SiedlungTexture siedlung = getSiedlungTextureByServerSiedlung(new Siedlung(new HexagonPosition(hexagonRowIndex, hexagonColumnIndex),
                                                                                              new HexagonPoint(hexagonPointIndex)), Color.Gray);
                                this.protoTypSiedlungen.Add(siedlung);
                            }
                        }
                    }
                }
            }

            this.shouldDrawProtoSiedlungen = true;
            this.protoTypSiedlungen.ForEach(protoSiedlung => Panel.Invalidate(protoSiedlung.Region));
        }

        public void DrawPrototypStrassen(bool [][][] strassen)
        {
            if (this.protoTypStrassen.Count <= 0)
            {
                for (int hexagonRowIndex = 0; hexagonRowIndex < strassen.GetLength(0); hexagonRowIndex++)
                {
                    for (int hexagonColumnIndex = 0; hexagonColumnIndex < strassen[hexagonRowIndex].GetLength(0); hexagonColumnIndex++)
                    {
                        for (int hexagonPointIndex = 0; hexagonPointIndex < strassen[hexagonRowIndex][hexagonColumnIndex].GetLength(0); hexagonPointIndex++)
                        {
                            if (strassen[hexagonRowIndex][hexagonColumnIndex][hexagonPointIndex])
                            {

                                StrasseTexture strasse = getStrasseTextureByServerStrasse(new Strasse(new HexagonPosition(hexagonRowIndex, hexagonColumnIndex),
                                                                                              new HexagonEdge(null,null,hexagonPointIndex)));
                                this.protoTypStrassen.Add(strasse);
                            }
                        }
                    }
                }
            }

            this.shouldDrawProtoStrassen = true;
            this.protoTypStrassen.ForEach(protoStrasse => Panel.Invalidate(protoStrasse.Region));
        }
        private SiedlungTexture getSiedlungTextureByServerSiedlung(Siedlung serverSiedlung, Color color)
        {
            int hexPointIndex = 0;
            var hexPoint = this.foregroundHexagones[serverSiedlung.HexagonPosition.RowIndex][serverSiedlung.HexagonPosition.ColumnIndex].
                Points[hexPointIndex = getClientHexagonPointIndexByServerHexagonPointIndex(serverSiedlung.HexagonPoint.Index)];

            return new SiedlungTexture(new HexagonPositionHexagonPoint(serverSiedlung.HexagonPosition, new HexagonPoint(hexPointIndex)),
                hexPoint.X, hexPoint.Y, 40, 40, new Pen(color));
        }

        private StrasseTexture getStrasseTextureByServerStrasse(Strasse serverStrasse,Color color)
        {
            int hexPointIndex = 0;
            var hexPoint = this.foregroundHexagones[serverSiedlung.HexagonPosition.RowIndex][serverSiedlung.HexagonPosition.ColumnIndex].
                Points[hexPointIndex = getClientHexagonPointIndexByServerHexagonPointIndex(serverSiedlung.HexagonPoint.Index)];

            return new StrasseTexture(new HexagonPositionHexagonPoint(serverSiedlung.HexagonPosition, new HexagonPoint(hexPointIndex)),
                hexPoint.X, hexPoint.Y, 40, 40, new Pen(color));
        }

        public void DrawSpielFiguren(List<CatanClient> clients)
        {
            this.catanClients = clients;
            Panel.Invalidate();
        }

    }
}
