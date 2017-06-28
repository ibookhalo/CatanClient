using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    public abstract class Texture
    {
        public float X { protected set; get; }
        public float Y { protected set; get; }
        public float Height { protected set; get; }
        public float Width { protected set; get; }
        public Pen Pen { protected set; get; }
        public Region Region
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(RegionPoints);
                return new Region(path);
            }
        }
        public abstract PointF[] RegionPoints { get; }
        public Texture(float x, float y,float height, float width,Pen pen)
        {
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;
            this.Pen = pen;
        }
        public abstract void Draw(Graphics graphics);

    }
}
