using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    class Edge
    {
        public PointF Point1 { private set; get; }
        public PointF Point2 { private set; get; }
        public float Length { get { return (float)Math.Sqrt(Math.Pow(Point2.X- Point1.X,2)  + Math.Pow(Point2.Y - Point1.Y, 2)); } }
        public Edge(PointF point1, PointF point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
        }
    }
}
