using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.UI
{
    public class Option<T> : BaseViewModel
    {
        public Option(string displayText, T value) : this(displayText, value, false) { }
        public Option(string displayText, T value, bool isSelected)
        {
            DisplayText = displayText;
            Value = value;
            IsSelected = isSelected;
        }

        private string _displayText;
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText == value) return;

                _displayText = value;

                OnPropertyChanged("DisplayText");
            }
        }

        public T Value { get; set; }

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
    }
}
