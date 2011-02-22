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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPS.UI.ViewModels;

namespace RPS.UI.Views
{
    /// <summary>
    /// Interaction logic for TournamentView.xaml
    /// </summary>
    public partial class TournamentView : UserControl
    {
        public TournamentView()
        {
            InitializeComponent();

            Loaded +=new RoutedEventHandler(TournamentView_Loaded);
        }

        void TournamentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (((App)App.Current).IsServer)
                DataContext = new ServerTournamentViewModel(Dispatcher);
            else
                DataContext = new TournamentViewModel(Dispatcher);
        }
    }
}
