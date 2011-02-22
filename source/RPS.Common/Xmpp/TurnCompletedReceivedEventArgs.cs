using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common.Xmpp
{
    public class TurnCompletedReceivedEventArgs : EventArgs
    {
        public string GameId { get; set; }

        public string Player1Move { get; set; }

        public string Player2Move { get; set; }

        public TurnResult Result { get; set; }
    }
}
