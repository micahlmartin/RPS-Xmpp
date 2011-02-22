using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using agsXMPP;
using RPS.UI.Views;

namespace RPS.UI.ViewModels
{
    public class XmppLogViewModel : BaseViewModel
    {
        private Dispatcher _dipatcher;
        private XmppLogView _view;
        private XmppClientConnection _xmppConnection;

        public XmppLogViewModel(Dispatcher dispatcher, XmppLogView view)
        {
            _dipatcher = dispatcher;
            _view = view;

            _xmppConnection = ((App)App.Current).XmppConnection;
            _xmppConnection.OnReadXml += XmppReadXmlHandler;
            _xmppConnection.OnWriteXml += XmppWriteXmlHandler;
            _view.Closed += new EventHandler(ViewClosedHandler);
        }

        public void XmppWriteXmlHandler(object sender, string xml)
        {
            _dipatcher.BeginInvoke(new Action(() =>
            {
                LogMessagesInternal.Add(xml);
            }), new object[] { });
        }

        private void XmppReadXmlHandler(object sender, string xml)
        {
            _dipatcher.BeginInvoke(new Action(() =>
            {
                LogMessagesInternal.Add(xml);
            }), new object[] { });
        }

        private void ViewClosedHandler(object sender, EventArgs e)
        {
            _xmppConnection.OnReadXml -= XmppReadXmlHandler;
            _xmppConnection.OnWriteXml -= XmppWriteXmlHandler;
        }

        private ObservableCollection<string> _logMessagseInternal;
        private ObservableCollection<string> LogMessagesInternal
        {
            get{
                if (_logMessagseInternal == null)
                    _logMessagseInternal = new ObservableCollection<string>();

                return _logMessagseInternal;
            }
        }
        private ReadOnlyObservableCollection<string> _logMessages;
        public ReadOnlyObservableCollection<string> LogMessages
        {
            get
            {
                if (_logMessages == null)
                    _logMessages = new ReadOnlyObservableCollection<string>(LogMessagesInternal);

                return _logMessages;
            }
        }
    }
}
