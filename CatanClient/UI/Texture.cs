using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    abstract class Texture
    {
        public int X { protected set; get; }
        public int Y { protected set; get; }
        public int Height { protected set; get; }
        public int Width { protected set; get; }


        public Texture(int x, int y,int height, int width)
        {
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;
        }
        public abstract void Draw(Graphics graphics);
    }
}
