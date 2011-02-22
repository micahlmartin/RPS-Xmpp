using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common.GameBot
{
    public class TournamentManagerBase
    {
        public void StartTournament(Settings settings)
        {
            Settings = settings;

            var games = SetupGames();

            OnTournamentStarted(games);
        }
        protected IEnumerable<GameInfo> SetupGames() { return Enumerable.Empty<GameInfo>(); }
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

        protected Settings Settings { get; private set; }
    }
}
