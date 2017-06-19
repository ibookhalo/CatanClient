using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Network.Messaging;
using Catan.Game;

namespace Catan.Client.Interfaces
{
    interface IPresentationLayer
    {
        void ThrowException(Exception ex);
        void UpdateGame(GameStateMessage gameStateMessage);
        void WaitForClientsToConnectWithServer();
        void InitGamePanel(Game.Hexagon[][] hexagonFields, List<CatanClient> clients);
    }
}
