using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatanClient.UI
{
    abstract class Texture
    {
        protected int X {  set; get; }
        protected int Y {  set; get; }
        protected int Height { set; get; }
        protected int Width { set; get; }


        public Texture(int x, int y,int height, int width)
        {
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;
        }

    }
}
