using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using agsXMPP;
using RPS.UI.Views;
using Microsoft.Practices.ServiceLocation;

namespace RPS.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Dispatcher _dispatcher;
        private XmppClientConnection _xmppConnection;
        private LoginView _view;

        private const string DefaultLoginButtonText = "Login";

        public LoginViewModel(Dispatcher dispatcher, LoginView view)
        {
            _dispatcher = dispatcher;
            _view = view;

            _xmppConnection = ((App)App.Current).XmppConnection;
            LoginCommand = new RelayCommand(Login, false);

            _xmppConnection.OnLogin += XmppLoginHandler;
            _xmppConnection.OnError += XmppErrorHandler;
            _xmppConnection.OnAuthError += XmppAuthErrorHandler;
        }

        private void XmppAuthErrorHandler(object sender, agsXMPP.Xml.Dom.Element e)
        {
            _dispatcher.BeginInvoke(new Action(() =>
            {
                ErrorMessage = "An error occurred trying to login.";
                HasError = true;
                LoginButtonText = DefaultLoginButtonText;
                SetCanLogin();
            }), new object[] { });
        }

        void XmppErrorHandler(object sender, Exception ex)
        {
            _dispatcher.BeginInvoke(new Action(() =>
            {
                ErrorMessage = "An error occurred trying to login.";
                HasError = true;
                LoginButtonText = DefaultLoginButtonText;
                SetCanLogin();
            }), new object[] { });
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value) return;

                _username = value;

                OnPropertyChanged("Username");

                SetCanLogin();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;

                _password = value;

                OnPropertyChanged("Password");

                SetCanLogin();
            }
        }

        private string _loginButtonText = DefaultLoginButtonText;
        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set
            {
                if (_loginButtonText == value) return;

                _loginButtonText = value;

                OnPropertyChanged("LoginButtonText");
            }
        }

        public RelayCommand LoginCommand { get; private set; }
        private void SetCanLogin()
        {
            LoginCommand.IsEnabled = !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
        private void Login()
        {
            LoginButtonText = "Authenticating...";
            LoginCommand.IsEnabled = false;

            if (IsServer)
                ((App)App.Current).SetupAsServer();
            else
                ((App)App.Current).SetupAsPlayer();

            ((App)App.Current).XmppHost.Connect(Username, Password);
        }
        private void XmppLoginHandler(object sender)
        {
            _xmppConnection.OnLogin -= XmppLoginHandler;
            _xmppConnection.OnError -= XmppErrorHandler;
            _xmppConnection.OnAuthError -= XmppAuthErrorHandler;

            _dispatcher.BeginInvoke(new Action(() => {

                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();

                var mainView = App.Current.MainWindow;
                mainView.Show();

                _view.Close();

            }), new object[] { });
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage == value) return;

                _errorMessage = value;

                OnPropertyChanged("ErrorMessage");
            }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                if (_hasError == value) return;

                _hasError = value;

                OnPropertyChanged("HasError");
            }
        }

        private bool _isServer;
        public bool IsServer
        {
            get { return _isServer; }
            set
            {
                if (_isServer == value) return;

                _isServer = value;

                OnPropertyChanged("IsServer");
            }
        }
    }
}
