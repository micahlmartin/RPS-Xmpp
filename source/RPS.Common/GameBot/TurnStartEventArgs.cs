using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPS.Common;

namespace RPS.Common.GameBot
{
    public class TurnStartEventArgs : EventArgs
    {
        public TurnStartEventArgs(GameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }

        public GameInfo GameInfo { get; set; }
    }
}
