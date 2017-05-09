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
        public static Bitmap ReplaceColor(Bitmap source,Color oldColor,Color newColor)
        {
            var target = new Bitmap(source.Width, source.Height);

            for (int x = 0; x < source.Width; ++x)
            {
                for (int y = 0; y < source.Height; ++y)
                {
                    var color = source.GetPixel(x, y);
                    target.SetPixel(x, y, color == oldColor ? newColor : color); 
                }
            }
            return target;
        }
        public static Image ResizeImageAndTransparent(Image img,Color transparentColor,int newWidth, int newHeight)
        {
            var bitMap = new Bitmap(img);
            bitMap.MakeTransparent(transparentColor);
            return ResizeImage(bitMap, newWidth, newHeight);
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
