using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    public class StrasseTexture : Texture
    {
        public HexagonPositionHexagonPoint HexagonPositionHexagonPoint { private set; get; }

        public StrasseTexture(float x, float y, float height, float width, Pen pen) : base(x, y, height, width, pen)
        {
        }

        public override PointF[] RegionPoints
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Draw(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
