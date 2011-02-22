using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common.Xmpp
{
    public class GameOverEventArgs : EventArgs
    {
        public GameOverEventArgs(string gameId, TurnResult winner)
        {
            GameId = gameId;
            Winner = winner;
        }

        public string GameId { get; set; }
        public TurnResult Winner { get; set; }
    }
}
