using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public static class GameHelper
    {
        public static TurnResult DetermineWinner(MoveBase player1Move, MoveBase player2Move)
        {
            if (player1Move == null || player2Move == null)
                return TurnResult.Unknown;

            if(player1Move.Equals(player2Move))
                return TurnResult.Tie;

            switch (player1Move.Name)
            {
                case MoveNames.NoMove:
                    return TurnResult.Player2;
                case MoveNames.Rock:
                    switch (player2Move.Name)
                    {
                        //Wins
                        case MoveNames.NoMove:
                        case MoveNames.Bubbles:
                        case MoveNames.Scissors:
                            return TurnResult.Player1;
                        //Loses
                        case MoveNames.Paper:
                        case MoveNames.Dynamite:
                            return TurnResult.Player2;
                        default:
                            throw new ArgumentException("Did not understand Player 2's move: " + player2Move.Name);
                    }
                case MoveNames.Paper:
                    switch (player2Move.Name)
                    {
                        //Wins
                        case MoveNames.Rock:
                        case MoveNames.NoMove:
                        case MoveNames.Bubbles:
                            return TurnResult.Player1;
                        //Loses
                        case MoveNames.Scissors:
                        case MoveNames.Dynamite:
                            return TurnResult.Player2;
                        default:
                            throw new ArgumentException("Did not understand Player 2's move: " + player2Move.Name);
                    }
                case MoveNames.Scissors:
                    switch (player2Move.Name)
                    {
                        //Wins
                        case MoveNames.Paper:
                        case MoveNames.NoMove:
                        case MoveNames.Bubbles:
                            return TurnResult.Player1;
                        //Loses
                        case MoveNames.Rock:
                        case MoveNames.Dynamite:
                            return TurnResult.Player2;
                        default:
                            throw new ArgumentException("Did not understand Player 2's move: " + player2Move.Name);
                    }
                case MoveNames.Dynamite:
                    switch (player2Move.Name)
                    {
                        //Wins
                        case MoveNames.Rock:
                        case MoveNames.Paper:
                        case MoveNames.Scissors:
                        case MoveNames.NoMove:
                            return TurnResult.Player1;
                        //Loses
                        case MoveNames.Bubbles:
                            return TurnResult.Player2;
                        default:
                            throw new ArgumentException("Did not understand Player 2's move: " + player2Move.Name);
                    }
                case MoveNames.Bubbles:
                    switch (player2Move.Name)
                    {
                        //Wins
                        case MoveNames.Dynamite:
                            return TurnResult.Player1;
                        //Loses
                        case MoveNames.NoMove:
                        case MoveNames.Paper:
                        case MoveNames.Rock:
                        case MoveNames.Scissors:
                            return TurnResult.Player2;
                        default:
                            throw new ArgumentException("Did not understand Player 2's move: " + player2Move.Name);
                    }
                default:
                    throw new ArgumentException("Did not understand Player 1's move: " + player1Move.Name);
            }
        }
    }
}
