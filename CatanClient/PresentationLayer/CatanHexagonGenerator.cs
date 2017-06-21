using Catan.Client.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    static class CatanHexagonGenerator
    {
        public static HexagonTexture[][] GetCatanHexagoneGrid(float x,float y,float radius,Pen pen)
        {
            // 7 Rows
            HexagonTexture[][] hexGrid = new HexagonTexture[5][];
            hexGrid[0] = new HexagonTexture[3]; // Row 0, Columns 3
            hexGrid[1] = new HexagonTexture[4];
            hexGrid[2] = new HexagonTexture[5];
            hexGrid[3] = new HexagonTexture[4];
            hexGrid[4] = new HexagonTexture[3];

            var hexProto = new HexagonTexture(x, y,0,0, radius, pen, true);

            x -= hexProto.Width-pen.Width;
            y -= hexProto.Height + (EdgeTexture.CalculateLength(hexProto.Points[2], hexProto.Points[1]))-pen.Width*2;

            float hexWidth = hexProto.Width;
            
            float xTemp = x;
            float yTemp = y;

            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    if (columnIndex > 0)
                        xTemp += hexWidth-pen.Width;

                    hexGrid[rowIndex][columnIndex] = new HexagonTexture(xTemp,yTemp,rowIndex, columnIndex, radius, pen, true);
                }
            
                if (rowIndex<2)
                {
                    xTemp = hexGrid[rowIndex][0].Points[5].X+pen.Width/2;
                    yTemp = hexGrid[rowIndex][0].Points[5].Y + radius-pen.Width;
                }
                else
                {
                    xTemp = hexGrid[rowIndex][0].Points[1].X - pen.Width / 2;
                    yTemp = hexGrid[rowIndex][0].Points[1].Y + radius - pen.Width;
                }
            }
           
            return hexGrid.ToArray();
        }
    }
}
