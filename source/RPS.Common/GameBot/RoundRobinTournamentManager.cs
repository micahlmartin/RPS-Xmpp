using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using RPS.Common.Messaging;
using RPS.Common.Xmpp;

namespace RPS.Common.GameBot
{
    public class RoundRobinTournamentManager
    {
        private IDictionary<GameInfo, SingleGameBot> _games;
        private IList<Jid> _registeredPlayers;
        private Settings _settings;
        private static object _lockObj = new Object();

        public RoundRobinTournamentManager()
        {
            _registeredPlayers = new List<Jid>();
        }

        public void StartTournament(Settings settings)
        {
            _settings = settings;

            SetupGames();

            OnTournamentStarted(_registeredPlayers.Select(x => x.ToString()), _games.Keys);

            foreach (var game in _games)
            {
                game.Value.StartGame(game.Key);
            }
        }
        private void SetupGames()
        {
            var games = new Dictionary<GameInfo, SingleGameBot>();

            for (int i = 0; i < _registeredPlayers.Count; i++)
            {
                var currentPlayer = _registeredPlayers[i];
                for (int j = i + 1; j < _registeredPlayers.Count; j++)
                {
                    var otherPlayer = _registeredPlayers[j];

                    var bot = new SingleGameBot();
                    games.Add(new GameInfo(_settings) { Player1 = currentPlayer.ToString(), Player2 = otherPlayer.ToString() }, bot);
                    OnGameBotCreated(bot);
                }
            }

            _games = games;
        }
        public event EventHandler<TournamentStartedEventArgs> TournamentStarted;
        private void OnTournamentStarted(IEnumerable<string> players, IEnumerable<GameInfo> games)
        {
            var tournamentStartedEvent = TournamentStarted;
            if (tournamentStartedEvent != null)
                tournamentStartedEvent(this, new TournamentStartedEventArgs(players, games.Select(x => new GameStartInfo
                {
                    Player1 = x.Player1,
                    GameId = x.GameId,
                    Player2 = x.Player2,
                    MaxMoves = x.MaxMoves,
                    AllowBubbles = x.AllowBubbles,
                    DynamiteCount = x.DynamiteCount
                })));
        }
        public event EventHandler<GameBotCreatedEventArgs> GameBotCreated;
        private void OnGameBotCreated(SingleGameBot gameBot)
        {
            var gameBotCreatedEvent = GameBotCreated;
            if (gameBotCreatedEvent != null)
                gameBotCreatedEvent(this, new GameBotCreatedEventArgs(gameBot));
        }

        public void ProcessPlayerMove(string gameId, string player, MoveBase move)
        {
            var game = _games.First(x => x.Key.GameId == gameId);
            game.Value.RecordResult(player, move);
        }

        public void RegisterPlayer(Jid user)
        {
            if (_registeredPlayers.Any(x => x.ToString() == user.ToString())) return;

            _registeredPlayers.Add(user);
        }

        public IDictionary<GameInfo, SingleGameBot> Games
        {
            get
            {
                return _games;
            }
        }
    }
}
