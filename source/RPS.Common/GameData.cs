using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public class GameStartInfo
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string GameId { get; set; }
        public int MaxMoves { get; set; }
        public int DynamiteCount { get; set; }
        public bool AllowBubbles { get; set; }
    }
}
