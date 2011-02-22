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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainView_Loaded);
        }

        void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel(Dispatcher);
        }
    }
}
