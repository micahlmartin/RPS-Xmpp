using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RPS.UI
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
