using Catan.Game;
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
        public EdgeTexture EdgeTexture { get; private set; }
        public HexagonPositionHexagonPoint HexagonPositionHexagonPoint { private set; get; }

        public StrasseTexture(HexagonPositionHexagonPoint hexagonPositionHexagonPoint,Catan.Client.PresentationLayer.EdgeTexture edgeTexture, Pen pen) : base(0, 0, 0, 0, pen)
        {
            this.EdgeTexture = edgeTexture;
            this.HexagonPositionHexagonPoint = hexagonPositionHexagonPoint;
        }
        public override PointF[] RegionPoints
        {
            get
            {
                return EdgeTexture.RegionPoints;
            }
        }
        public override void Draw(Graphics graphics)
        {
            EdgeTexture edge;
            (edge=(EdgeTexture.Clone() as EdgeTexture)).Pen.Color = Pen.Color;
            edge.Draw(graphics);
            
        }
    }
}
