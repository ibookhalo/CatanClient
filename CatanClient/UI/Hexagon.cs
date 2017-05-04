
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CatanClient.UI
{
    class Hexagon:Texture
    {
        public List<Edge> Edges { private set; get; }
        public Point[] Points { get { return new Point[] {Edges[0].Point1, Edges[1].Point1, Edges[2].Point1, Edges[3].Point1, Edges[4].Point1, Edges[5].Point1 }; } }

        private Image backgroundImage;
        private bool isBackgroundImageProccessed;
        private Pen pen;

        public Hexagon(int x, int y,int height,int width,Pen pen,Image backgroundImage) : base(x, y,height, width)
        {
            this.Edges = new List<Edge>();
            this.backgroundImage = backgroundImage;
            this.isBackgroundImageProccessed = false;
            this.pen = pen;
            int radius = heightWidth / 2;
            radius -=(int)pen.Width / 2;
            
            List<Point> points = new List<Point>();
            for (int i = 0; i < 6; i++)
            {
                double xx = x + radius * Math.Sin((Math.PI / 3) * i);
                double yy = y + radius * Math.Cos((Math.PI / 3) * i);

                points.Add(new Point((int)xx, (int)yy));
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
            if (backgroundImage!=null)
            {
                if (!isBackgroundImageProccessed)
                {
                    backgroundImage = ImageHelper.ResizeImage(backgroundImage, Width, Height);
                    backgroundImage = ImageHelper.GetImageWithArea(backgroundImage,new List<Point> (new Hexagon(0, 0,Height,pen,null).Points));

                    isBackgroundImageProccessed = true;

                    graphics.DrawImage(backgroundImage, X, Y);
                   
                }
            }

            graphics.DrawPolygon(pen, Points);
        }
    }
}
