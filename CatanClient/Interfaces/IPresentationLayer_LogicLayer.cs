using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Catan.Client.PresentationLayer;
using System.Net.Mail;

namespace Catan.Client.Interfaces
{
    interface IPresentationLayer_LogicLayer
    {
        void ConnectToCatanServerAsync(IPAddress serverIp,string mail, string password, string playerName);
        int GetMyClientID();
        void BaueSiedlung(SiedlungTexture siedlung);
        void BaueStrasse(StrasseTexture clickedStrasse);
        void TurnDone();
    }
}
