using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using RPS.Common.Messaging;
using RPS.Common;
using System.Xml.Linq;
using RPS.Common.GameBot;

namespace RPS.Common.Xmpp
{
    public class XmppTournamentManagerHost : XmppHostBase
    {
        
        public XmppTournamentManagerHost(RoundRobinTournamentManager tournamentManager, XmppClientConnection xmppConnection) : base(xmppConnection)
        {
            TournamentManager = tournamentManager;
            TournamentManager = tournamentManager;
            TournamentManager.TournamentStarted += TournamentStartedHandler;
            TournamentManager.GameBotCreated += GameBotCreatedHandler;
        }

        public RoundRobinTournamentManager TournamentManager { get; private set; }

        private void GameBotCreatedHandler(object sender, GameBotCreatedEventArgs e)
        {
            e.GameBot.Error += GameErrorHandler;
            e.GameBot.GameStarted += GameStartedHandler;
            e.GameBot.GameOver += GameOverHandler;
            e.GameBot.TurnStarted += TurnStartedHandler;
            e.GameBot.TurnCompleted += TurnCompletedHandler;
        }

        private void TournamentStartedHandler(object sender, TournamentStartedEventArgs e)
        {
            var tournamentStartedMessageXml = new TournamentStartedMessage
            {
                Games = e.Games.Select(x => new GameStartMessage
                {
                    GameId = x.GameId,
                    MaxMoves = x.MaxMoves,
                    Player1 = x.Player1,
                    Player2 = x.Player2
                }).ToArray()
            }.ToXml();

            foreach (var player in e.Players)
            {
                Connection.Send(new Message(player, tournamentStartedMessageXml));
            }
        }

        private void TurnCompletedHandler(object sender, TurnCompletedEventArgs e)
        {
            var turnCompletedXml = new TurnResultMessage
            {
                Player1Move = e.TurnData.Player1Move.Name,
                Player2Move = e.TurnData.Player2Move.Name,
                Result = e.TurnData.Result,
                GameId = e.GameInfo.GameId
            }.ToXml();

            Connection.Send(new Message(e.GameInfo.Player1, turnCompletedXml));
            Connection.Send(new Message(e.GameInfo.Player2, turnCompletedXml));
        }

        private void TurnStartedHandler(object sender, TurnStartEventArgs e)
        {
            var turnStartXml = new TurnStartMessage
            {
                GameId = e.GameInfo.GameId
            }.ToXml();

            Connection.Send(new Message(e.GameInfo.Player1, turnStartXml));
            Connection.Send(new Message(e.GameInfo.Player2, turnStartXml));
        }

        private void GameOverHandler(object sender, GameBot.GameOverEventArgs e)
        {
            var gameOverXml = new GameOverMessage
            {
                GameId = e.GameInfo.GameId,
                Winner = e.GameInfo.GameWinner
            }.ToXml();

            Connection.Send(new Message(e.GameInfo.Player1, gameOverXml));
            Connection.Send(new Message(e.GameInfo.Player2, gameOverXml));
        }

        private void GameStartedHandler(object sender, GameStartEventArgs e)
        {
            var gameStartXml = new GameStartMessage
            {
                MaxMoves = e.GameInfo.MaxMoves,
                Player1 = e.GameInfo.Player1,
                Player2 = e.GameInfo.Player2,
                GameId = e.GameInfo.GameId,
                AllowBubbles = e.GameInfo.AllowBubbles,
                DynamiteCount = e.GameInfo.DynamiteCount
            }.ToXml();

            Connection.Send(new Message(e.GameInfo.Player1, gameStartXml));
            Connection.Send(new Message(e.GameInfo.Player2, gameStartXml));
        }

        private void GameErrorHandler(object sender, ErrorEventArgs e)
        {
            Connection.Send(new Message(e.Player, e.ErrorMessage));
        }

        protected override void ProcessPlayerMoveMessage(Jid player, PlayerMoveMessage playerMoveMessage)
        {
            TournamentManager.ProcessPlayerMove(playerMoveMessage.GameId, player.ToString(), MoveFactory.GetMove(playerMoveMessage.Move));

            base.ProcessPlayerMoveMessage(player, playerMoveMessage);
        }

        protected override void ProcessRegisterMessage(Jid player)
        {
            TournamentManager.RegisterPlayer(player);
            Connection.Send(new Message(player, new RegistrationCompleteMessage().ToXml()));

            base.ProcessRegisterMessage(player);
        }
    }
}
