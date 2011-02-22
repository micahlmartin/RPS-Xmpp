using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using RPS.Common.Xmpp;
using RPS.UI;
using agsXMPP;
using RPS.Common.GameBot;

namespace RPS.UI.ViewModels
{
    public class TournamentViewModel : BaseViewModel
    {
        private Dispatcher _dispatcher;
        private XmppHostBase _xmppHost;

        public TournamentViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _xmppHost = ((App)App.Current).XmppHost;
            _xmppHost.TurnCompleted += new EventHandler<TurnCompletedReceivedEventArgs>(TurnCompletedEventHandler);
            _xmppHost.TournamentStarted += TournamentStartedHandler;

            ShowTournamentWait = true;
            ShowTournamentList = false;

            if (((App)App.Current).IsServer)
            {
                ShowTournamentList = true;
                ShowTournamentWait = false;
            }
        }

        private void TurnCompletedEventHandler(object sender, TurnCompletedReceivedEventArgs e)
        {
            _dispatcher.BeginInvoke(new Action(() =>
            {
                var game = TournamentGames.FirstOrDefault(x => x.GameId == e.GameId);
                if (game == null) return;

                if (e.Result == Common.TurnResult.Player1)
                    game.Player1Wins++;
                else if (e.Result == Common.TurnResult.Player2)
                    game.Player2Wins++;
                else if (e.Result == Common.TurnResult.Tie)
                    game.Ties++;

                game.Turns.Add(new TurnResultViewModel
                {
                    Player1Move = e.Player1Move,
                    Player2Move = e.Player2Move,
                    Result = e.Result.ToString()
                });

            }), new object[] { });

        }

        private void TournamentStartedHandler(object sender, TournamentStartedEventArgs e)
        {
            ShowTournamentWait = false;
            ShowTournamentList = true;
            _dispatcher.BeginInvoke(new Action(() => {
                foreach (var game in e.Games)
                {
                    if (new Jid(game.Player1).Bare == _xmppHost.Connection.MyJID.Bare || new Jid(game.Player2).Bare == _xmppHost.Connection.MyJID.Bare || ((App)App.Current).IsServer)
                    {
                        TournamentGamesInternal.Add(new TournamentGameItemViewModel
                        {
                            GameId = game.GameId,
                            MaxMoves = game.MaxMoves,
                            Player1 = new Jid(game.Player1).Bare,
                            Player2 = new Jid(game.Player2).Bare
                        });
                    }
                }
            }), new object[] { });
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
