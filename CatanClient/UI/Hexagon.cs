﻿
using System;
using System.Collections.Generic;
using System.Drawing;

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

        public Hexagon(float x, float y,float radius,Pen pen,Image backgroundImage, bool isPickTop) : base(x, y,0, 0)
        {
            this.Edges = new List<Edge>();
            this.backgroundImage = backgroundImage;
            this.isBackgroundImageProccessed = false;
            this.pen = pen;
            this.Radius = radius;
            this.isPickTop = isPickTop;
           
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

            if (isPickTop)
            {
                this.Width = new Edge(points[2], points[4]).Length;
                this.Height = 2 * radius;
            }
            else
            {
                this.Width = 2 * radius;
                this.Height= this.Width = new Edge(points[2], points[4]).Length;
            }

            Edges.Add(new UI.Edge(points[0], points[1]));
            Edges.Add(new UI.Edge(points[1], points[2]));
            Edges.Add(new UI.Edge(points[2], points[3]));
            Edges.Add(new UI.Edge(points[3], points[4]));
            Edges.Add(new UI.Edge(points[4], points[5]));
            Edges.Add(new UI.Edge(points[5], points[0]));
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
            graphics.DrawPolygon(pen, new Hexagon(X, Y, Radius - pen.Width / 2, pen, null, isPickTop).Points);
        }
    }
}
