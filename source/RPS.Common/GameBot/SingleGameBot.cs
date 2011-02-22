using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using RPS.Common;
using System.Timers;

namespace RPS.Common.GameBot
{
    public class SingleGameBot
    {
        private GameInfo _game;
        private Settings _settings;
        private Timer _timer;
        public SingleGameBot() { }

        public string GameId
        {
            get { return _game.GameId; }
        }

        public void StartGame(string player1, string player2, Settings settings)
        {
            _settings = settings;
            _game = new GameInfo(settings) { Player1 = player1, Player2 = player2 };
            OnGameStarted(_game);
            _game.NewTurn();
            OnTurnStarted(_game);
        }

        public void StartGame(GameInfo game)
        {
            if (_timer == null)
            {
                _timer = new Timer(10000);
                _timer.Elapsed += TimeElapsed;
            }

            _game = game;
            OnGameStarted(_game);
            _game.NewTurn();
            OnTurnStarted(_game);
        }

        public void RecordResult(string player, MoveBase move)
        {
            try
            {
                var turnResult = _game.RecordTurn(player, move);
                if (turnResult.Result != TurnResult.Unknown)
                {
                    if(_timer.Enabled)
                        _timer.Stop();

                    if (_game.IsGameOver)
                        OnGameOver(_game);
                    else
                    {
                        OnTurnCompleted(_game, turnResult);
                        _game.NewTurn();
                        OnTurnStarted(_game);
                    }
                }
            }
            catch (Exception ex)
            {
                OnGameError(player, ex.Message);
            }
        }

        public event EventHandler<GameStartEventArgs> GameStarted;
        protected void OnGameStarted(GameInfo gameInfo)
        {
            var gameStartedEvent = GameStarted;
            if (gameStartedEvent != null)
                gameStartedEvent(this, new GameStartEventArgs(gameInfo));
        }

        public event EventHandler<TurnStartEventArgs> TurnStarted;
        protected void OnTurnStarted(GameInfo gameInfo)
        {
            _timer.Start();
            var turnStartedEvent = TurnStarted;
            if (turnStartedEvent != null)
                turnStartedEvent(this, new TurnStartEventArgs(gameInfo));
        }
        private void TimeElapsed(object sender, ElapsedEventArgs e)
        {
            if (_timer.Enabled)
                _timer.Stop();

            var currentTurn = _game.CurrentTurn;

            if (currentTurn.Player1Move == null)
                RecordResult(_game.Player1, new NoMove());
            if (currentTurn.Player2Move == null)
                RecordResult(_game.Player2, new NoMove());
        }

        public event EventHandler<TurnCompletedEventArgs> TurnCompleted;
        protected void OnTurnCompleted(GameInfo gameInfo, TurnData data)
        {
            var turnCompletedEvent = TurnCompleted;
            if (turnCompletedEvent != null)
                turnCompletedEvent(this, new TurnCompletedEventArgs(gameInfo, data));
        }

        public event EventHandler<ErrorEventArgs> Error;
        protected void OnGameError(string player, string errorMessage)
        {
            var errorEvent = Error;
            if (errorEvent != null)
                errorEvent(this, new ErrorEventArgs(player, errorMessage));
        }

        public event EventHandler<GameOverEventArgs> GameOver;
        protected void OnGameOver(GameInfo gameInfo)
        {
            var gameOverEvent = GameOver;
            if (gameOverEvent != null)
                gameOverEvent(this, new GameOverEventArgs(gameInfo));
        }
    }
}
