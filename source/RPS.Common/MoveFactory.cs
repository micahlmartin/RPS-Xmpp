using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public static class MoveFactory
    {
        public static MoveBase GetMove(string moveName)
        {
            switch (moveName)
            {
                case MoveNames.Rock:
                    return new Rock();
                case MoveNames.Paper:
                    return new Paper();
                case MoveNames.Scissors:
                    return new Scissors();
                case MoveNames.NoMove:
                    return new NoMove();
                case MoveNames.Bubbles:
                    return new Bubbles();
                case MoveNames.Dynamite:
                    return new Dynamite();
                default:
                    throw new ArgumentException("Did not understand the move: " + moveName);
            }
        }
    }
}
