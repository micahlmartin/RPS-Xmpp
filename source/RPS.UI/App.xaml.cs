using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using agsXMPP;
using RPS.Common.Xmpp;
using RPS.Common;
using SamplePlayerBot;
using RPS.Common.GameBot;

namespace RPS.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public XmppClientConnection XmppConnection { get; private set; }
        public RoundRobinTournamentManager TournamentManager { get; private set; }
        public XmppHostBase XmppHost { get; private set; }
        public bool IsServer { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            XmppConnection = new XmppClientConnection();
            XmppConnection.AutoResolveConnectServer = true;
        }

        public void SetupAsServer()
        {
            TournamentManager = new RoundRobinTournamentManager();
            XmppHost = new XmppTournamentManagerHost(TournamentManager, XmppConnection);
            XmppConnection = XmppHost.Connection;

            SetupErrrorHandling();

            IsServer = true;
        }

        public void SetupAsPlayer()
        {
            Func<IPlayerBot> createNewBotFunc = () => {
                //TODO: Create your bot here
                return new SimpleBot();
            };

            XmppHost = new XmppPlayerBotHost(createNewBotFunc, XmppConnection);
            XmppConnection = XmppHost.Connection;

            SetupErrrorHandling();

            IsServer = false;
        }

        private void SetupErrrorHandling()
        {
            XmppConnection.OnAuthError += new XmppElementHandler(XmppConnection_OnAuthError);
            XmppConnection.OnError += new ErrorHandler(XmppConnection_OnError);
            XmppConnection.OnSocketError += new ErrorHandler(XmppConnection_OnSocketError);
            XmppConnection.OnStreamError += new XmppElementHandler(XmppConnection_OnStreamError);
        }

        private void XmppConnection_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
        }
        private void XmppConnection_OnSocketError(object sender, Exception ex)
        {
        }
        private void XmppConnection_OnError(object sender, Exception ex)
        {
        }
        private void XmppConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
        }
    }
}
