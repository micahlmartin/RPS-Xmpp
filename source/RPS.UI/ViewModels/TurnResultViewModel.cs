using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.UI.ViewModels
{
    public class TurnResultViewModel : BaseViewModel
    {
        private string _player1Move;
        public string Player1Move
        {
            get { return _player1Move; }
            set
            {
                if (_player1Move == value) return;

                _player1Move = value;

                OnPropertyChanged("Player1Move");
            }
        }

        private string _player2Move;
        public string Player2Move
        {
            get { return _player2Move; }
            set
            {
                if (_player2Move == value) return;

                _player2Move = value;

                OnPropertyChanged("Player2Move");
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                if (_result == value) return;

                _result = value;

                OnPropertyChanged("Result");
            }
        }
    }
}
