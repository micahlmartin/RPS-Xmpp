using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using RPS.Common.Messaging;
using RPS.Common.GameBot;

namespace RPS.Common.Xmpp
{
    public class XmppHostBase
    {
        private XmppClientConnection _xmppConnection;

        public XmppHostBase(XmppClientConnection xmppConnection)
        {
            _xmppConnection = xmppConnection;
            _xmppConnection.AutoResolveConnectServer = true;
            _xmppConnection.OnMessage += (sender, message) => { ProcessMessage(message); };
        }

        public XmppClientConnection Connection { get { return _xmppConnection; } }

        public virtual void Connect(string username, string password)
        {
            var jid = new Jid(username);
            _xmppConnection.Username = jid.User;
            _xmppConnection.Server = jid.Server;
            _xmppConnection.Password = password;
            _xmppConnection.Open();
        }

        protected virtual void ProcessMessage(Message message)
        {
            if (message.Body == null) return;

            try
            {
                var msgObj = MessageFactory.GetMessageFromXml(message.Body);
                if (msgObj == null) return;

                var msgType = msgObj.GetType();

                if (msgType == typeof(GameStartMessage))
                    ProcessGameStartMessage(message.From, (GameStartMessage)msgObj);
                if (msgType == typeof(GameOverMessage))
                    ProcessGameOverMessage(message.From, (GameOverMessage)msgObj);
                if (msgType == typeof(TurnResultMessage))
                    ProcessTurnResultMessage(message.From, (TurnResultMessage)msgObj);
                if (msgType == typeof(TurnStartMessage))
                    ProcessTurnStartMessage(message.From, (TurnStartMessage)msgObj);
                if (msgType == typeof(RegistrationCompleteMessage))
                    ProcessRegistrationCompleteMessage(message.From);
                if (msgType == typeof(TournamentStartedMessage))
                    ProcessTournamentStartedMessage(message.From, (TournamentStartedMessage)msgObj);
                if (msgType == typeof(PlayerMoveMessage))
                    ProcessPlayerMoveMessage(message.From, (PlayerMoveMessage)msgObj);
                if (msgType == typeof(RegisterMessage))
                    ProcessRegisterMessage(message.From);
            }
            catch (Exception) { }
        }

        #region << Message Handlers >>

        protected virtual void ProcessTournamentStartedMessage(Jid from, TournamentStartedMessage tournamentStartedMessage)
        {
            OnTournamentStarted(tournamentStartedMessage.Games.Select(x =>
            {
                return new GameStartInfo
                {
                    GameId = x.GameId,
                    MaxMoves = x.MaxMoves,
                    Player1 = x.Player1,
                    Player2 = x.Player2
                };
            }));
        }
        public event EventHandler<TournamentStartedEventArgs> TournamentStarted;
        private void OnTournamentStarted(IEnumerable<GameStartInfo> games)
        {
            var tournamentStartedEvent = TournamentStarted;
            if (tournamentStartedEvent != null)
                tournamentStartedEvent(this, new TournamentStartedEventArgs(null, games));
        }

        protected virtual void ProcessRegistrationCompleteMessage(Jid from)
        {
            OnRegistrationComplete();
        }
        public event EventHandler RegistrationComplete;
        private void OnRegistrationComplete()
        {
            var registrationCompleteEvent = RegistrationComplete;
            if (registrationCompleteEvent != null)
                registrationCompleteEvent(this, EventArgs.Empty);
        }

        protected virtual void ProcessTurnStartMessage(Jid from, TurnStartMessage turnStartMessage) { }

        protected virtual void ProcessTurnResultMessage(Jid from, TurnResultMessage turnResultMessage)
        {
            OnTurnCompleted(turnResultMessage);
        }
        public event EventHandler<TurnCompletedReceivedEventArgs> TurnCompleted;
        private void OnTurnCompleted(TurnResultMessage turnResultMessage)
        {
            var turnCompletedEvent = TurnCompleted;
            if (turnCompletedEvent != null)
                turnCompletedEvent(this, new TurnCompletedReceivedEventArgs
                {
                    GameId = turnResultMessage.GameId,
                    Player1Move = turnResultMessage.Player1Move,
                    Player2Move = turnResultMessage.Player2Move,
                    Result = turnResultMessage.Result
                });
        }

        protected virtual void ProcessGameOverMessage(Jid from, GameOverMessage gameOverMessage) 
        {
            OnGameOver(gameOverMessage.GameId, gameOverMessage.Winner);
        }
        public event EventHandler<GameOverEventArgs> GameOver;
        private void OnGameOver(string gameId, TurnResult winner)
        {
            var gameOverEvent = GameOver;
            if (gameOverEvent != null)
                gameOverEvent(this, new GameOverEventArgs(gameId, winner));
        }

        protected virtual void ProcessGameStartMessage(Jid from, GameStartMessage gameStartMessage) { }

        protected virtual void ProcessPlayerMoveMessage(Jid jid, PlayerMoveMessage playerMoveMessage) { }

        protected virtual void ProcessRegisterMessage(Jid player)
        {
            OnPlayerRegistered(player);
        }
        public event EventHandler<RegisterEventArgs> PlayerRegistered;
        protected void OnPlayerRegistered(Jid player)
        {
            var playerRegisteredEvent = PlayerRegistered;
            if (playerRegisteredEvent != null)
                playerRegisteredEvent(this, new RegisterEventArgs(player));
        }

        #endregion
    }
}
