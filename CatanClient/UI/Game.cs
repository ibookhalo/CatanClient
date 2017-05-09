using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatanClient.UI
{
    class Game
    {
        private Form form;
        private GamePanel gamePanel;
        public Game(Form form)
        {
            this.form = form;
        }

        public  void Run()
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            gamePanel = new GamePanel();
            form.Controls.Add(gamePanel.Panel);
        }
    }
}
