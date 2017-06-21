using Catan.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Client.PresentationLayer
{
    public class PlayerControlEventArg:EventArgs
    {
        public Game.CatanClient Client { get; private set; }

        public PlayerControlEventArg(Game.CatanClient client)
        {
            this.Client = client;
        }
    }
}
