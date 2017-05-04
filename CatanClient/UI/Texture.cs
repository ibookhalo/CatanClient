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
        public float X { protected set; get; }
        public float Y { protected set; get; }
        public float Height { protected set; get; }
        public float Width { protected set; get; }


        public Texture(float x, float y,float height, float width)
        {
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;
        }
        public abstract void Draw(Graphics graphics);
    }
}
