using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Client.EventArgs
{
    public class ClientConnectedToServerErrorEventArgs
    {
        public Exception Exception { private set; get; }
        public ClientConnectedToServerErrorEventArgs(Exception ex)
        {
            this.Exception = ex;
        }
    }
}
