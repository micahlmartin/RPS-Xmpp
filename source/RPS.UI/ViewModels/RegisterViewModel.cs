using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPS.Common.Xmpp;

namespace RPS.UI.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private XmppPlayerBotHost _botHost;

        public RegisterViewModel()
        {
            _botHost = (XmppPlayerBotHost)((App)App.Current).XmppHost;

            JoinTournamentCommand = new RelayCommand(JoinTournament, false);
            ShowTournamentLoadingProgress = false;
            ShowJoinTournament = true;
        }

        public RelayCommand JoinTournamentCommand { get; private set; }
        public void JoinTournament()
        {
            //ShowJoinTournament = false;
            //ShowTournamentLoadingProgress = true;
            _botHost.JoinTournament(TournamentServerId);
        }
        private void SetCanJoinEnabled()
        {
            JoinTournamentCommand.IsEnabled = !string.IsNullOrWhiteSpace(TournamentServerId);/* && 
                !string.IsNullOrWhiteSpace(TournamentId) &&
                !string.IsNullOrWhiteSpace(TeamName);*/
        }

        private string _tournamentServerId;
        public string TournamentServerId
        {
            get { return _tournamentServerId; }
            set
            {
                if (_tournamentServerId == value) return;

                _tournamentServerId = value;

                OnPropertyChanged("TournamentServerId");

                SetCanJoinEnabled();
            }
        }

        private bool _showTournamentLoadingProgress;
        public bool ShowTournamentLoadingProgress
        {
            get { return _showTournamentLoadingProgress; }
            set
            {
                if (_showTournamentLoadingProgress == value) return;

                _showTournamentLoadingProgress = value;

                OnPropertyChanged("ShowTournamentLoadingProgress");
            }
        }

        private bool _showJoinTournament;
        public bool ShowJoinTournament
        {
            get { return _showJoinTournament; }
            set
            {
                if (_showJoinTournament == value) return;

                _showJoinTournament = value;

                OnPropertyChanged("ShowJoinTournament");
            }
        }

        private bool _joinTournamentErrorMessage;
        public bool JoinTournamentErrorMessage
        {
            get { return _joinTournamentErrorMessage; }
            set
            {
                if (_joinTournamentErrorMessage == value) return;

                _joinTournamentErrorMessage = value;

                OnPropertyChanged("JoinTournamentErrorMessage");
            }
        }

        private bool _hasJoinTournamentError;
        public bool HasJoinTournamentError
        {
            get { return _hasJoinTournamentError; }
            set
            {
                if (_hasJoinTournamentError == value) return;

                _hasJoinTournamentError = value;

                OnPropertyChanged("HasJoinTournamentError");
            }
        }

        //private string _tournamentId;
        //public string TournamentId
        //{
        //    get { return _tournamentId; }
        //    set
        //    {
        //        if (_tournamentId == value) return;

        //        _tournamentId = value;

        //        OnPropertyChanged("TournamentId");

        //        SetCanJoinEnabled();
        //    }
        //}

        //private string _teamName;
        //public string TeamName
        //{
        //    get { return _teamName; }
        //    set
        //    {
        //        if (_teamName == value) return;

        //        _teamName = value;

        //        OnPropertyChanged("TeamName");

        //        SetCanJoinEnabled();
        //    }
        //}
    }
}
