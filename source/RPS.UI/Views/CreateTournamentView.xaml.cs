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
    /// Interaction logic for CreateTournamentView.xaml
    /// </summary>
    public partial class CreateTournamentView : UserControl
    {
        public CreateTournamentView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(CreateTournamentView_Loaded);
        }

        void CreateTournamentView_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new CreateTournamentViewModel(Dispatcher);
        }
    }
}
