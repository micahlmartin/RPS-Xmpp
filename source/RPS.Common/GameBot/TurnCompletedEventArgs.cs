using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPS.Common;

namespace RPS.Common.GameBot
{
    public class TurnCompletedEventArgs : EventArgs
    {
        public TurnCompletedEventArgs(GameInfo gameInfo, TurnData data)
        {
            GameInfo = gameInfo;
            TurnData = data;
        }

        public TurnData TurnData { get; set; }
        public GameInfo GameInfo { get; set; }
    }
}
