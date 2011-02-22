using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using RPS.Common.Xmpp;
using RPS.Common.GameBot;

namespace RPS.UI.ViewModels
{
    public class ServerTournamentViewModel : BaseViewModel
    {
        private Dispatcher _dispatcher;
        private XmppTournamentManagerHost _xmppHost;

        public ServerTournamentViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _xmppHost = (XmppTournamentManagerHost)((App)App.Current).XmppHost;

            ShowTournamentList = true;
            _showTournamentWait = false;

            InitializeGameData();
        }

        private void InitializeGameData()
        {
            foreach (var kvpGame in _xmppHost.TournamentManager.Games)
            {
                kvpGame.Value.GameStarted += GameStartedHandler;
                kvpGame.Value.GameOver += GameOverHandler;
                kvpGame.Value.TurnCompleted += TurnCompletedHandler;

                TournamentGamesInternal.Add(new TournamentGameItemViewModel
                {
                    GameId = kvpGame.Key.GameId,
                    MaxMoves = kvpGame.Key.MaxMoves,
                    Player1 = kvpGame.Key.Player1,
                    Player2 = kvpGame.Key.Player2

                });
            }
        }

        private void GameBotCreatedHandler(object sender, GameBotCreatedEventArgs e)
        {
            e.GameBot.GameStarted += new EventHandler<GameStartEventArgs>(GameStartedHandler);
            e.GameBot.GameOver += new EventHandler<Common.GameBot.GameOverEventArgs>(GameOverHandler);
            e.GameBot.TurnCompleted += new EventHandler<TurnCompletedEventArgs>(TurnCompletedHandler);
        }

        private void TurnCompletedHandler(object sender, TurnCompletedEventArgs e)
        {
            _dispatcher.BeginInvoke(new Action(() =>
            {
                var game = TournamentGames.FirstOrDefault(x => x.GameId == e.GameInfo.GameId);
                if (game == null) return;

                if (e.TurnData.Result == Common.TurnResult.Player1)
                    game.Player1Wins++;
                else if (e.TurnData.Result == Common.TurnResult.Player2)
                    game.Player2Wins++;
                else if (e.TurnData.Result == Common.TurnResult.Tie)
                    game.Ties++;

                game.Turns.Add(new TurnResultViewModel
                {
                    Player1Move = e.TurnData.Player1Move.Name,
                    Player2Move = e.TurnData.Player2Move.Name,
                    Result = e.TurnData.Result.ToString()
                });

            }), new object[] { });
        }

        private void GameOverHandler(object sender, Common.GameBot.GameOverEventArgs e)
        {
        }

        private void GameStartedHandler(object sender, GameStartEventArgs e)
        {
            
        }

        private bool _showTournamentWait;
        public bool ShowTournamentWait
        {
            get { return _showTournamentWait; }
            set
            {
                if (_showTournamentWait == value) return;

                _showTournamentWait = value;

                OnPropertyChanged("ShowTournamentWait");
            }
        }

        private bool _showTournamentList;
        public bool ShowTournamentList
        {
            get { return _showTournamentList; }
            set
            {
                if (_showTournamentList == value) return;

                _showTournamentList = value;

                OnPropertyChanged("ShowTournamentList");
            }
        }

        private ObservableCollection<TournamentGameItemViewModel> _tournamentGamesInternal;
        private ObservableCollection<TournamentGameItemViewModel> TournamentGamesInternal
        {
            get
            {
                if (_tournamentGamesInternal == null)
                    _tournamentGamesInternal = new ObservableCollection<TournamentGameItemViewModel>();

                return _tournamentGamesInternal;
            }
        }
        private ReadOnlyObservableCollection<TournamentGameItemViewModel> _tournamentGames;
        public ReadOnlyObservableCollection<TournamentGameItemViewModel> TournamentGames
        {
            get
            {
                if (_tournamentGames == null)
                    _tournamentGames = new ReadOnlyObservableCollection<TournamentGameItemViewModel>(TournamentGamesInternal);

                return _tournamentGames;
            }
        }
        
    }
}
