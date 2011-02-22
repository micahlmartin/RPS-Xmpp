using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using RPS.Common.Xmpp;
using System.Windows.Threading;
using agsXMPP;
using RPS.Common;
using RPS.UI.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace RPS.UI.ViewModels
{
    public class CreateTournamentViewModel : BaseViewModel
    {
        private XmppHostBase _xmppHost;
        private Dispatcher _dispatcher;
        private GameController _gameController;

        public CreateTournamentViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _xmppHost = ((App)App.Current).XmppHost;
            _gameController = ServiceLocator.Current.GetInstance<GameController>();

            StartTournamentCommand = new RelayCommand(StartTournament);

            TournamentTypeListInternal.Add(new Option<string>("Round Robin", "RoundRobin"));
            _xmppHost.PlayerRegistered += (sender, e) => { _dispatcher.BeginInvoke(() => { RegisteredPlayerListInternal.Add(new Option<Jid>(e.User.Bare, e.User)); UpdateCanStartTournament(); }); };
        }

        private ObservableCollection<Option<string>> _tournamentTypeListInternal;
        private ObservableCollection<Option<string>> TournamentTypeListInternal
        {
            get
            {
                if (_tournamentTypeListInternal == null)
                    _tournamentTypeListInternal = new ObservableCollection<Option<string>>();

                return _tournamentTypeListInternal;
            }
        }
        private ReadOnlyObservableCollection<Option<string>> _tournamentTypeList;
        public ReadOnlyObservableCollection<Option<string>> TournamentTypeList
        {
            get
            {
                if (_tournamentTypeList == null)
                    _tournamentTypeList = new ReadOnlyObservableCollection<Option<string>>(TournamentTypeListInternal);

                return _tournamentTypeList;
            }
        }

        private ObservableCollection<Option<Jid>> _registeredPlayerListInternal;
        private ObservableCollection<Option<Jid>> RegisteredPlayerListInternal
        {
            get
            {
                if (_registeredPlayerListInternal == null)
                    _registeredPlayerListInternal = new ObservableCollection<Option<Jid>>();

                return _registeredPlayerListInternal;
            }
        }
        private ReadOnlyObservableCollection<Option<Jid>> _registeredPlayerList;
        public ReadOnlyObservableCollection<Option<Jid>> RegisteredPlayerList
        {
            get
            {
                if (_registeredPlayerList == null)
                    _registeredPlayerList = new ReadOnlyObservableCollection<Option<Jid>>(RegisteredPlayerListInternal);

                return _registeredPlayerList;
            }
        }

        private int _roundCount = 500;
        public int RoundCount
        {
            get { return _roundCount; }
            set
            {
                if (_roundCount == value) return;

                _roundCount = value;

                OnPropertyChanged("RoundCount");

                UpdateCanStartTournament();
            }
        }

        public RelayCommand StartTournamentCommand { get; private set; }
        private void StartTournament()
        {
            ((App)App.Current).TournamentManager.StartTournament(new Settings { MaxMoves = RoundCount, AllowBubbles = AllowBubbles, DynamiteCount = DynamiteCount });
            _gameController.CloseCreateTournamentView();
            _gameController.ShowTournamentView();
        }
        private void UpdateCanStartTournament()
        {
            StartTournamentCommand.IsEnabled = RegisteredPlayerList.Count > 1 && RoundCount > 0;
        }

        private bool _allowBubbles;
        public bool AllowBubbles
        {
            get { return _allowBubbles; }
            set
            {
                if (_allowBubbles == value) return;

                _allowBubbles = value;

                OnPropertyChanged("AllowBubbles");
            }
        }

        private int _dynamiteCount;
        public int DynamiteCount
        {
            get { return _dynamiteCount; }
            set
            {
                if (_dynamiteCount == value) return;

                _dynamiteCount = value;

                OnPropertyChanged("DynamiteCount");
            }
        }
    }
}
