using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public class Settings
    {
        public Settings()
        {
            TurnTimeout = 10;
            MaxMoves = 1000;
        }

        /// <summary>
        /// Number of seconds a player has to make a turn. Default is 10.
        /// </summary>
        public int TurnTimeout { get; set; }

        /// <summary>
        /// The number of Moves per game. Default is 1000
        /// </summary>
        public int MaxMoves { get; set; }

        /// <summary>
        /// Amount of Dynamite that can be used
        /// </summary>
        public int DynamiteCount { get; set; }


        /// <summary>
        /// Specifies whether or not Bubles can be used.
        /// </summary>
        public bool AllowBubbles { get; set; }
    }
}
