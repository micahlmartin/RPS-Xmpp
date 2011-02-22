using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPS.Common;

namespace RPS.Common.GameBot
{
    public class GameOverEventArgs : EventArgs
    {
        public GameOverEventArgs(GameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }

        public GameInfo GameInfo { get; set; }
    }
}
