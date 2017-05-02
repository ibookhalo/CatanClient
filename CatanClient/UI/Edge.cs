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
        public Point Point1 { private set; get; }
        public Point Point2 { private set; get; }
        public Edge(Point point1, Point point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
        }
    }
}
