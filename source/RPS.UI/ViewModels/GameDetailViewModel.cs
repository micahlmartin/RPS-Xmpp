using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RPS.UI.ViewModels
{
    public class GameDetailViewModel : BaseViewModel
    {
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
