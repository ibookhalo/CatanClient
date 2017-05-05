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

            /*
            float xTemp = 0;
            float yTemp = 0;

            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    if (columnIndex>0)
                        xTemp += radius;

                    hexGrid[rowIndex][columnIndex] = new Hexagon(xTemp,yTemp, radius, pen,backgroundImage);
                }
                if (rowIndex<3)
                {
                    xTemp = hexGrid[rowIndex][0].Points[4].X - (width / 2);
                    yTemp = hexGrid[rowIndex][0].Points[4].Y;
                }
                else
                {
                    xTemp = hexGrid[rowIndex][0].Points[4].X + (width / 2);
                    yTemp = hexGrid[rowIndex][0].Points[4].Y;
                }
            }*/
           
            return hexGrid.ToArray();
        }
    }
}
