using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public class GameInfo
    {
        private IList<TurnData> _turnData;
        private Settings _settings;

        public GameInfo(Settings settings)
        {
            GameId = Guid.NewGuid().ToString("N");
            _settings = settings;
            _turnData = new List<TurnData>();
        }

        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string GameId { get; private set; }

        /// <summary>
        /// Determines the number of moves for the game
        /// </summary>
        public int MaxMoves { get { return _settings.MaxMoves; } }

        public int DynamiteCount { get { return _settings.DynamiteCount; } }

        public bool AllowBubbles { get { return _settings.AllowBubbles; } }

        public int CurrentMoveCount
        {
            get { return _turnData.Count; }
        }

        public TurnData CurrentTurn
        {
            get
            {
                return _turnData.Last();
            }
        }

        public void NewTurn()
        {
            _turnData.Add(new TurnData());
        }

        public TurnData RecordTurn(string player, MoveBase move)
        {
            move = EnforceGameRules(player == Player1, move);

            var currentTurn = CurrentTurn;

            if (Player1 == player)
            {
                if (currentTurn.Player1Move != null)
                    throw new ArgumentException(string.Format("Player 1 ({0}) already made a move.", player));

                currentTurn.Player1Move = move;
            }
            else if (Player2 == player)
            {
                if (currentTurn.Player2Move != null)
                    throw new ArgumentException(string.Format("Player 2 ({0}) already made a move.", player));

                currentTurn.Player2Move = move;
            }
            else
                throw new ArgumentException(string.Format("Player ({0}) is not playing game ({1})", player, GameId));

            return currentTurn;
        }

        private MoveBase EnforceGameRules(bool isPlayer1, MoveBase move)
        {
            //Can't throw bubbles if they're not allowed
            if (move.Name == MoveNames.Bubbles && !_settings.AllowBubbles)
                return new NoMove();

            //Can't throw dynamite if it's not allowed
            if (move.Name == MoveNames.Dynamite)
            {
                 if(_settings.DynamiteCount == 0)
                    return new NoMove();

                int dynamiteTurns = 0;
                if(isPlayer1)
                    dynamiteTurns = _turnData.Count(x => x.Player1Move != null && x.Player1Move.Name == MoveNames.Dynamite);
                else
                    dynamiteTurns = _turnData.Count(x => x.Player2Move != null && x.Player2Move.Name == MoveNames.Dynamite);

                if (dynamiteTurns >= _settings.DynamiteCount)
                    return new NoMove();
            }



            return move;
        }

        public bool IsGameOver
        {
            get
            {
                if (_turnData.Count <= MaxMoves) return false;

                return CurrentTurn.Result != TurnResult.Unknown;
            }
        }

        public TurnResult GameWinner
        {
            get
            {
                if (!IsGameOver)
                    return TurnResult.Unknown;

                var player1WinCount = _turnData.Count(x => x.Result == TurnResult.Player1);
                var player2WinCount = _turnData.Count(x => x.Result == TurnResult.Player2);

                if (player1WinCount == player2WinCount)
                    return TurnResult.Tie;
                if (player1WinCount > player2WinCount)
                    return TurnResult.Player1;

                return TurnResult.Player2;
            }
        }
    }
}
