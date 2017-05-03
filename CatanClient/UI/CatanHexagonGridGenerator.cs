using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    static class CatanHexagonGridGenerator
    {
        public static Hexagone[][] GetCatanHexagoneGrid(int x,int y,int height, int width,Pen pen,Image backgroundImage)
        {
            // 7 Rows
            Hexagone[][] hexGrid = new Hexagone[7][];
            hexGrid[0] = new Hexagone[3]; // Row 0, Columns 3
            hexGrid[1] = new Hexagone[4];
            hexGrid[2] = new Hexagone[5];
            hexGrid[3] = new Hexagone[6];
            hexGrid[4] = new Hexagone[5];
            hexGrid[5] = new Hexagone[4];
            hexGrid[6] = new Hexagone[3];

            int yTemp = y;
            int xTemp = x;

            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    if (columnIndex>0)
                        xTemp += width;

                    hexGrid[rowIndex][columnIndex] = new Hexagone(xTemp, yTemp, height, width,pen,backgroundImage);
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
            }
           
            return hexGrid.ToArray();
        }
    }
}
