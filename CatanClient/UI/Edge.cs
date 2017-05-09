using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    class Edge:Texture
    {
        public PointF Point1 { private set; get; }
        public PointF Point2 { private set; get; }
        public PointF Point3 { private set; get; }
        public PointF Point4 { private set; get; }
        public float Length { get { return CalculateLength(Point1, Point2); } }
        public Pen Pen{ private set; get; }
        public override Region Region
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(new PointF[] { Point1, Point2, Point4, Point3 });
                return new Region(path);
            }
        }

        public Edge(PointF point1, PointF point2, PointF point3, PointF point4,Pen pen):base(point1.X,point1.Y, Math.Abs(point1.Y - point3.Y), Math.Abs(point2.X-point1.X))
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.Point3 = point3;
            this.Point4 = point4;
            this.Pen = pen;
        }
        public static float CalculateLength(PointF point1,PointF point2)
        {
            return (float)Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }
        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(Pen, new PointF[] { Point1, Point2, Point4, Point3 });
        }
    }
}
