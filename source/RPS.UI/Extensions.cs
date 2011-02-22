using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace RPS.UI
{
    public static class Extensions
    {
        public static void BeginInvoke(this Dispatcher dispatcher, Action action)
        {
            dispatcher.BeginInvoke(action, new object[] { });
        }
    }
}
