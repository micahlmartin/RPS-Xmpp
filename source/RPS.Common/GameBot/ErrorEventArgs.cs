using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common.GameBot
{
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(string player, string message)
        {
            Player = player;
            ErrorMessage = message;
        }

        public string Player { get; set; }
        public string ErrorMessage { get; set; }
    }
}
