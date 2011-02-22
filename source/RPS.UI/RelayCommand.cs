using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace RPS.UI
{
    public class RelayCommand : ICommand
    {
        private Action _handler;
        private bool _isEnabled;

        public RelayCommand(Action handler) : this(handler, true) { }
        public RelayCommand(Action handler, bool isEnabled)
        {
            _handler = handler;
            IsEnabled = isEnabled;
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                        CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }
    }
}
