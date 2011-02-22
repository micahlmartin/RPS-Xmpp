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
    /// Interaction logic for XmppLogView.xaml
    /// </summary>
    public partial class XmppLogView : Window
    {
        public XmppLogView()
        {
            InitializeComponent();

            DataContext = new XmppLogViewModel(Dispatcher, this);
        }
    }
}
