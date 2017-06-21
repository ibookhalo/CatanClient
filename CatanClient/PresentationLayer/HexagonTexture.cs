
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Catan.Client.PresentationLayer
{
    class HexagonTexture:Texture
    {
        private bool isBackgroundImageProccessed;
        private bool isPickTop;

        private Image backgroundImage;
        public Image BackgroundImage
        {
            set
            {
                if (backgroundImage != value)
                {
                    this.backgroundImage = value;
                    isBackgroundImageProccessed = false;
                }
            }
            get
            {
                return backgroundImage;
            }
        }
        public int Nr {set; get; }
        public List<EdgeTexture> Edges { private set; get; }
        public PointF[] Points { get { return new PointF[] {Edges[0].Point1, Edges[1].Point1, Edges[2].Point1, Edges[3].Point1, Edges[4].Point1, Edges[5].Point1 }; } }
        public float Radius { private set; get; }
        public int ColumnIndex { private set; get; }
        public int RowIndex { private set; get; }
        public override PointF[] RegionPoints
        {
            get
            {
                return Points;
            }
        }
        public HexagonTexture(float x, float y,int rowIndex, int columnIndex, float radius,Pen pen,bool isPickTop) : base(x, y,0, 0,pen)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;

            this.Edges = new List<EdgeTexture>();

            this.isBackgroundImageProccessed = false;
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            this.Radius = radius;
            this.isPickTop = isPickTop;

            var pointsOutHex = getHexagonPoints(x, y, radius, isPickTop);
            var pointsInHex = getHexagonPoints(x, y, radius-pen.Width, isPickTop);

            if (isPickTop)
            {
                this.Width = EdgeTexture.CalculateLength(pointsOutHex[2], pointsOutHex[4]);
                this.Height = 2 * radius;
            }
            else
            {
                this.Width = 2 * radius;
                this.Height= this.Width = EdgeTexture.CalculateLength(pointsOutHex[2], pointsOutHex[4]);
            }
           
           
            Edges.Add(new EdgeTexture(pointsOutHex[0], pointsOutHex[1], pointsInHex[0], pointsInHex[1], pen));
            Edges.Add(new EdgeTexture(pointsOutHex[1], pointsOutHex[2], pointsInHex[1], pointsInHex[2], pen));
            Edges.Add(new EdgeTexture(pointsOutHex[2], pointsOutHex[3], pointsInHex[2], pointsInHex[3], pen));
            Edges.Add(new EdgeTexture(pointsOutHex[3], pointsOutHex[4], pointsInHex[3], pointsInHex[4], pen));
            Edges.Add(new EdgeTexture(pointsOutHex[4], pointsOutHex[5], pointsInHex[4], pointsInHex[5], pen));
            Edges.Add(new EdgeTexture(pointsOutHex[5], pointsOutHex[0], pointsInHex[5], pointsInHex[0], pen));
        }
        private PointF[] getHexagonPoints(float x, float y,float radius,bool isPickTop)
        {
            List<PointF> points = new List<PointF>();

            for (int pointIndex = 0; pointIndex < 6; pointIndex++)
            {
                if (isPickTop)
                {
                    points.Add(new PointF(
                    (float)(x + radius * Math.Sin((pointIndex * Math.PI) / 3)),
                    (float)(y + radius * Math.Cos((pointIndex * Math.PI) / 3))));
                }
                else
                {
                    points.Add(new PointF(
                    (float)(x + radius * Math.Cos((pointIndex * Math.PI) / 3)),
                    (float)(y + radius * Math.Sin((pointIndex * Math.PI) / 3))));
                }
            }
            return points.ToArray();
        }
        public override void Draw(Graphics graphics)
        {
            #region Image
            if (backgroundImage!= null)
            {
                if (!isBackgroundImageProccessed)
                {
                    backgroundImage = ImageHelper.ResizeImage(backgroundImage, (int)Width, (int)Height);
                    backgroundImage = ImageHelper.GetImageWithArea(backgroundImage, new List<PointF>(new HexagonTexture(backgroundImage.Width / 2, backgroundImage.Height / 2, 0, 0, Radius - Pen.Width / 2, Pen, isPickTop).Points));
                    isBackgroundImageProccessed = true;
                }

                graphics.DrawImage(backgroundImage, X - backgroundImage.Width / 2, Y - backgroundImage.Height / 2);
            }
            #endregion

            #region Hexagon

            graphics.DrawPolygon(Pen, Points);

            #endregion

            #region Nr String

            string nrString = Nr.ToString();
            var font = new System.Drawing.Font("Agency FB",30F,FontStyle.Bold);
            var fontSize = graphics.MeasureString(nrString, font);

            var rectFont =new RectangleF(X - fontSize.Width / 2, Y -fontSize.Height / 2, fontSize.Width, fontSize.Height);

            graphics.DrawRectangle(new Pen(Color.White,0.1F),rectFont.X,rectFont.Y,rectFont.Width,rectFont.Height);
            graphics.FillRectangle(Brushes.White, rectFont);
            
            graphics.DrawString(Nr.ToString(),font ,Brushes.Black,rectFont);

            #endregion
        }
    }
}
