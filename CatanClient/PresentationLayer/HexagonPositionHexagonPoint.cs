using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    public class HexagonPositionHexagonPoint
    {

        public HexagonPoint HexagonPoint { get; private set; }
        public HexagonPosition HexagonPosition { get; private set; }

        public HexagonPositionHexagonPoint(HexagonPosition hexagonPosition, HexagonPoint hexagonPoint)
        {
            this.HexagonPosition = hexagonPosition;
            this.HexagonPoint = hexagonPoint;
        }

    }
}
