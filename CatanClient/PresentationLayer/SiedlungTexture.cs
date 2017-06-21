using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    class SiedlungTexture : Texture
    {
        public SiedlungTexture(float x, float y, float height, float width, Pen pen) : base(x, y, height, width, pen)
        {
            // translate x, y to center
            X -= Width / 2;
            Y -= Height / 2;
        }

        public override PointF[] RegionPoints
        {
            get
            {
                float halfWidth = Width / 2;
                float halfHeight = Height / 2;

                return new PointF[] {new PointF(X, Y),new PointF(X+Width,Y),
                                     new PointF(X+Width,Y+Height),new PointF(X,Y+Height)};
            }
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(ImageLoader.GetSiedlungImage(Pen.Color, (int)Height, (int)Width), new PointF(X, Y));
        }
    }
}
