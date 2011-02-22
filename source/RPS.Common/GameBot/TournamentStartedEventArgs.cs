using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common.GameBot
{
    public class TournamentStartedEventArgs : EventArgs
    {
        public TournamentStartedEventArgs(IEnumerable<string> players, IEnumerable<GameStartInfo> games)
        {
            Players = players;
            Games = games;
        }

        public IEnumerable<GameStartInfo> Games { get; set; }

        public IEnumerable<string> Players { get; set; }
    }
}
