using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPS.UI.Views;
using RPS.Common.Xmpp;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using RPS.UI.Controllers;

namespace RPS.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private XmppHostBase _botHost;
        private GameController _gameController;
        private Dispatcher _dispatcher;

        public MainViewModel(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            _botHost = ((App)App.Current).XmppHost;
            _botHost.RegistrationComplete += RegistrationCompleteHandler;
            OpenXmppLogViewCommand = new RelayCommand(OpenXmppLogView, true);
            ExitApplicationCommand = new RelayCommand(ExitApplication, true);
            WindowTitle = "RPS - Connected as " + _botHost.Connection.MyJID.Bare;
            _gameController = ServiceLocator.Current.GetInstance<GameController>();

            if (((App)App.Current).IsServer)
                _gameController.ShowCreateTournamentView();
            else
                _gameController.ShowRegisterView();
        }

        private void RegistrationCompleteHandler(object sender, EventArgs e)
        {
            _dispatcher.BeginInvoke(new Action(() => {
                _gameController.CloseRegisterView();
                _gameController.ShowTournamentView();
            }), new object[] { });
        }

        public RelayCommand OpenXmppLogViewCommand { get; private set; }
        private XmppLogView _xmppLogView;
        public void OpenXmppLogView()
        {
            if (_xmppLogView != null && _xmppLogView.IsLoaded)
            {
                _xmppLogView.WindowState = System.Windows.WindowState.Normal;
                _xmppLogView.Activate();
                return;
            }

            _xmppLogView = new XmppLogView();
            _xmppLogView.Show();
            _xmppLogView.Activate();
        }

        public RelayCommand ExitApplicationCommand { get; private set; }
        public void ExitApplication()
        {
            App.Current.Shutdown();
        }

        private string _windowTitle;
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                if (_windowTitle == value) return;

                _windowTitle = value;

                OnPropertyChanged("WindowTitle");
            }
        }
    }
}
