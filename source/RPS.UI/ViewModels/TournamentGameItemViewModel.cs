using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RPS.UI.ViewModels
{
    public class TournamentGameItemViewModel : BaseViewModel
    {
        private string _player1;
        public string Player1
        {
            get { return _player1; }
            set
            {
                if (_player1 == value) return;

                _player1 = value;

                OnPropertyChanged("Player1");
            }
        }

        private string _player2;
        public string Player2
        {
            get { return _player2; }
            set
            {
                if (_player2 == value) return;

                _player2 = value;

                OnPropertyChanged("Player2");
            }
        }

        private string _gameId;
        public string GameId
        {
            get { return _gameId; }
            set
            {
                if (_gameId == value) return;

                _gameId = value;

                OnPropertyChanged("GameId");
            }
        }

        private int _maxMoves;
        public int MaxMoves
        {
            get { return _maxMoves; }
            set
            {
                if (_maxMoves == value) return;

                _maxMoves = value;

                OnPropertyChanged("MaxMoves");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;

                _isSelected = value;

                OnPropertyChanged("IsSelected");
            }
        }

        private int _player1Wins;
        public int Player1Wins
        {
            get { return _player1Wins; }
            set
            {
                if (_player1Wins == value) return;

                _player1Wins = value;

                OnPropertyChanged("Player1Wins");
            }
        }

        private int _player2Wins;
        public int Player2Wins
        {
            get { return _player2Wins; }
            set
            {
                if (_player2Wins == value) return;

                _player2Wins = value;

                OnPropertyChanged("Player2Wins");
            }
        }

        private int _ties;
        public int Ties
        {
            get { return _ties; }
            set
            {
                if (_ties == value) return;

                _ties = value;

                OnPropertyChanged("Ties");
            }
        }

        private ObservableCollection<TurnResultViewModel> _turns;
        public ObservableCollection<TurnResultViewModel> Turns
        {
            get
            {
                if (_turns == null)
                    _turns = new ObservableCollection<TurnResultViewModel>();

                return _turns;
            }
        }
    }
}
