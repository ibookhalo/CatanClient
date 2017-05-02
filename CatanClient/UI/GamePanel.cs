using System;
using System.Drawing;
using System.Windows.Forms;

namespace CatanClient.UI
{
    class GamePanel:Panel
    {
        public GamePanel()
        {
            Dock = DockStyle.Fill;
            BackColor = System.Drawing.Color.Wheat;
            Paint += GamePanel_Paint;
            MouseClick += GamePanel_MouseClick;
        }

        private void GamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            int row, col;
            PointToHex(e.X, e.Y, 100,out row,out col);
            Console.WriteLine($"Row {row}, Col {col}");
        }

        private float HexWidth(float height)
        {
            return (float)(4 * (height / 2 / Math.Sqrt(3)));
        }
        private PointF[] HexToPoints(float height, float row, float col)
        {
            // Start with the leftmost corner of the upper left hexagon.
            float width = HexWidth(height);
            float y = height / 2;
            float x = 0;

            // Move down the required number of rows.
            y += row * height;

            // If the column is odd, move down half a hex more.
            if (col % 2 == 1) y += height / 2;

            // Move over for the column number.
            x += col * (width * 0.75f);

            // Generate the points.
            return new PointF[]
                {
            new PointF(x, y),
            new PointF(x + width * 0.25f, y - height / 2),
            new PointF(x + width * 0.75f, y - height / 2),
            new PointF(x + width, y),
            new PointF(x + width * 0.75f, y + height / 2),
            new PointF(x + width * 0.25f, y + height / 2),
                };
        }
        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            // Draw polygon to screen.
            e.Graphics.DrawPolygon(blackPen, HexToPoints(100,0,0));
            e.Graphics.DrawPolygon(blackPen, HexToPoints(100, 0, 1));
            e.Graphics.DrawPolygon(blackPen, HexToPoints(100, 0, 2));
            e.Graphics.DrawPolygon(blackPen, HexToPoints(100, 1, 0));
            e.Graphics.DrawPolygon(blackPen, HexToPoints(100, 1, 1));
        }
    }
}
