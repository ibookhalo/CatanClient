using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catan.Client.PresentationLayer
{
    class CustomPanel:Panel
    {
        public CustomPanel()
        {
            this.BackColor = System.Drawing.Color.White;

            this.SetStyle(
             System.Windows.Forms.ControlStyles.UserPaint |
             System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
             System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
             true);

        }
    }
}
