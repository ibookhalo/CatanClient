using CatanClient.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatanClient
{
    public partial class FormTest : Form
    {

        public FormTest()
        {
            InitializeComponent();
            panelImages.Paint += Panel_Paint;

            /*
            Panel p2 = new Panel();
            p2.Dock = DockStyle.Fill;
            //p2.BackColor = Color.Transparent;
            panelImages.Controls.Add(p2);
            p2.Paint += P2_Paint;
            */
        }

        private void P2_Paint(object sender, PaintEventArgs e)
        {


            int penWidth = 15;
            Hexagone[][] hexGrid = CatanHexagonGridGenerator.GetCatanHexagoneGrid(400, 0, 150, 150, penWidth);

            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    
                    e.Graphics.FillPolygon(Brushes.Transparent, hexGrid[rowIndex][columnIndex].Points);
                    e.Graphics.DrawPolygon(new Pen(Color.Black, penWidth), hexGrid[rowIndex][columnIndex].Points);
                }
            }
        }
        private Bitmap MakeImageWithArea(Bitmap source_bm,
     List<Point> points)
        {
            // Copy the image.
            Bitmap bm = new Bitmap(source_bm.Width, source_bm.Height);

            // Clear the selected area.
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.Transparent);

                // Make a brush that contains the original image.
                using (Brush brush = new TextureBrush(source_bm))
                {
                    // Fill the selected area.
                    gr.FillPolygon(brush, points.ToArray());
                }
            }
            return bm;
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            int penWidth =15;
            Image bitmap = Image.FromFile("test.jpeg");
            int hexW, hexH;
            hexW = hexH = 150;
            Hexagone[][] hexGrid = CatanHexagonGridGenerator.GetCatanHexagoneGrid(250,0,150,150, penWidth);

            bitmap = ResizeImage(bitmap, hexW, hexH);
            bitmap = MakeImageWithArea(bitmap as Bitmap, new Hexagone(0,0,150,150,penWidth).Points.ToList());


            for (int rowIndex = 0; rowIndex < hexGrid.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < hexGrid[rowIndex].GetLength(0); columnIndex++)
                {
                    
                    e.Graphics.DrawImage(bitmap, hexGrid[rowIndex][columnIndex].X, hexGrid[rowIndex][columnIndex].Y);
                    e.Graphics.DrawPolygon(new Pen(Color.Black, penWidth), hexGrid[rowIndex][columnIndex].Points);
                }
            }
            
        }
    }
}
