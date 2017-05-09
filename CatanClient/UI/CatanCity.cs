using CatanClient.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    class CatanCity : Texture
    {
        public Color Color { set; get; }
        public Image Image { private set; get; }
        private static Image mainImager;
        public override Region Region
        {
            get
            {
                GraphicsPath path = new GraphicsPath();
                var point1 = new PointF(X - Width / 2, Y - Height / 2);
                var point2 = new PointF(point1.X + Width, point1.Y);
                var point3 = new PointF(point2.X, point2.Y + Height);
                var point4 = new PointF(point1.X, point3.Y);

                path.AddPolygon(new PointF[] {point1, point2,point3 ,point4});
                return new Region(path);
            }
        }

        public CatanCity(float x, float y, float height, float width,Color color) : base(x, y, height, width)
        {
            Color = color;
        }

        public CatanCity(float x, float y, Image image,Color color) : this(x, y, image.Height, image.Width,color)
        {
            this.Image = image;
        }

        public override void Draw(Graphics graphics)
        {
                mainImager= ImageHelper.ResizeImageAndTransparent(Resources.HouseGray, Color.White, (int)Height, (int)Width) as Bitmap;
         
            Image = ImageHelper.ReplaceColor(mainImager as Bitmap, Color.FromArgb(195, 195, 195), Color);
            graphics.DrawImage(Image, X - Width / 2, Y - Height / 2);
        }
    }
}
