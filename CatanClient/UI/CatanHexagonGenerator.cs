using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    static class CatanHexagonGenerator
    {
        public static Hexagon[][] GetCatanHexagoneGrid(float x,float y,float radius,Pen pen,Image backgroundImage)
        {
            // 7 Rows
            Hexagon[][] hexGrid = new Hexagon[5][];
            hexGrid[0] = new Hexagon[3]; // Row 0, Columns 3
            hexGrid[1] = new Hexagon[4];
            hexGrid[2] = new Hexagon[5];
            hexGrid[3] = new Hexagon[4];
            hexGrid[4] = new Hexagon[3];

            var hexProto = new Hexagon(x, y, radius, pen, null, true);

            x -= hexProto.Width;
            y -= hexProto.Height + (new Edge(hexProto.Points[2],hexProto.Points[1]).Length);

            float hexWidth = hexProto.Width;
            
            float xTemp = x;
            float yTemp = y;

            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    if (columnIndex > 0)
                        xTemp += hexWidth;

                    hexGrid[rowIndex][columnIndex] = new Hexagon(xTemp,yTemp, radius, pen,backgroundImage,true);
                }
                if (rowIndex<2)
                {
                    xTemp = hexGrid[rowIndex][0].Points[5].X;
                    yTemp = hexGrid[rowIndex][0].Points[5].Y + radius;
                }
                else
                {
                    xTemp = hexGrid[rowIndex][0].Points[1].X ;
                    yTemp = hexGrid[rowIndex][0].Points[1].Y+radius;
                }
            }
           
            return hexGrid.ToArray();
        }
    }
}
