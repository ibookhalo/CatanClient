
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CatanClient.UI
{
    class Hexagon:Texture
    {
        public List<Edge> Edges { private set; get; }
        public PointF[] Points { get { return new PointF[] {Edges[0].Point1, Edges[1].Point1, Edges[2].Point1, Edges[3].Point1, Edges[4].Point1, Edges[5].Point1 }; } }
        

        private Image backgroundImage;
        private bool isBackgroundImageProccessed;
        private Pen pen;
        private bool isPickTop;
        public float Radius { private set; get; }

        public override Region Region
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(Points);
                return new Region(path);
            }
        }

        public Hexagon(float x, float y,float radius,Pen pen,Image backgroundImage, bool isPickTop) : base(x, y,0, 0)
        {
            this.Edges = new List<Edge>();
            this.backgroundImage = backgroundImage;
            this.isBackgroundImageProccessed = false;
            this.pen = pen;pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            this.Radius = radius;
            this.isPickTop = isPickTop;

            var pointsOutHex = getHexagonPoints(x, y, radius, isPickTop);
            var pointsInHex = getHexagonPoints(x, y, radius-pen.Width, isPickTop);

            if (isPickTop)
            {
                this.Width = Edge.CalculateLength(pointsOutHex[2], pointsOutHex[4]);
                this.Height = 2 * radius;
            }
            else
            {
                this.Width = 2 * radius;
                this.Height= this.Width = Edge.CalculateLength(pointsOutHex[2], pointsOutHex[4]);
            }
           
           
            Edges.Add(new UI.Edge(pointsOutHex[0], pointsOutHex[1], pointsInHex[0], pointsInHex[1],pen));
            Edges.Add(new UI.Edge(pointsOutHex[1], pointsOutHex[2], pointsInHex[1], pointsInHex[2], pen));
            Edges.Add(new UI.Edge(pointsOutHex[2], pointsOutHex[3], pointsInHex[2], pointsInHex[3], pen));
            Edges.Add(new UI.Edge(pointsOutHex[3], pointsOutHex[4], pointsInHex[3], pointsInHex[4], pen));
            Edges.Add(new UI.Edge(pointsOutHex[4], pointsOutHex[5], pointsInHex[4], pointsInHex[5], pen));
            Edges.Add(new UI.Edge(pointsOutHex[5], pointsOutHex[0], pointsInHex[5], pointsInHex[0], pen));
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
            if (backgroundImage!=null && !isBackgroundImageProccessed)
            {
                backgroundImage = ImageHelper.ResizeImage(backgroundImage, (int)Width, (int)Height);

                backgroundImage = ImageHelper.GetImageWithArea(backgroundImage, new List<PointF>(new Hexagon(backgroundImage.Width / 2, backgroundImage.Height / 2, Radius - pen.Width / 2, pen, null, isPickTop).Points));
                isBackgroundImageProccessed = true;
                graphics.DrawImage(backgroundImage, X - backgroundImage.Width / 2, Y - backgroundImage.Height / 2);
            }
            graphics.DrawPolygon(pen, Points);
        }
    }
}
