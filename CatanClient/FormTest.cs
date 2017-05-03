﻿using CatanClient.Properties;
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
        Game game;

        public FormTest()
        {
            InitializeComponent();

            game = new Game(this);
            game.Run();
            
        }
    }
}
