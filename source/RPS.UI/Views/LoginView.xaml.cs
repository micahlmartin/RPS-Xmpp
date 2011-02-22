using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RPS.UI.ViewModels;

namespace RPS.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel _vm;

        public LoginView()
        {
            InitializeComponent();

            _vm = new LoginViewModel(Dispatcher, this);

            DataContext = _vm;
        }

        private void PasswordChangedHandler(object sender, RoutedEventArgs e)
        {
            _vm.Password = txtPassword.Password;
        }


    }
}
