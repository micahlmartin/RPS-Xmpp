using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using RPS.UI.Views;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace RPS.UI
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.TryResolve<MainView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
        }
    }
}
