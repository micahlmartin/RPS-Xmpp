using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common.GameBot
{
    public class GameBotCreatedEventArgs : EventArgs
    {
        public GameBotCreatedEventArgs(SingleGameBot gameBot)
        {
            GameBot = gameBot;
        }


        public SingleGameBot GameBot { get; set; }
    }
}
