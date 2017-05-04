using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    static class ImageHelper
    {
        public static Image GetImageWithArea(Image sourceBitmap,List<PointF> points)
        {
            Bitmap newBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.Clear(Color.Transparent);

                using (Brush brush = new TextureBrush(sourceBitmap))
                {
                    graphics.FillPolygon(brush, points.ToArray());
                }
            }
            return newBitmap;
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

    }
}
