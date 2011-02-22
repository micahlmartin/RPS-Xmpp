using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public class TurnData
    {
        public string GameId { get; set; }
        public MoveBase Player1Move { get; set; }
        public MoveBase Player2Move { get; set; }
        public TurnResult Result
        {
            get
            {
                if (Player1Move == null || Player1Move == null)
                    return TurnResult.Unknown;

                return GameHelper.DetermineWinner(Player1Move, Player2Move);
            }
        }
    }
}
