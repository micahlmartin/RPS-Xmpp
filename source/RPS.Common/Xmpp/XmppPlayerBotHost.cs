using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using System.Xml.Linq;
using RPS.Common.Messaging;
using RPS.Common;
using agsXMPP.Collections;
using agsXMPP.protocol.x.muc;

namespace RPS.Common.Xmpp
{
    public class XmppPlayerBotHost : XmppHostBase
    {
        private IDictionary<string, IPlayerBot> _bots;
        private Func<IPlayerBot> _createBotFunc;

        public XmppPlayerBotHost(Func<IPlayerBot> createBotFunc,  XmppClientConnection xmppConnection) : base(xmppConnection) 
        {
            _createBotFunc = createBotFunc;
        }

        public void JoinTournament(string server)
        {
            Connection.Send(new Message(new Jid(server), new RegisterMessage().ToXml()));
        }

        protected override void ProcessTurnStartMessage(Jid from, TurnStartMessage turnStartMessage)
        {
            var move = _bots[turnStartMessage.GameId].MakeMove();

            var messageXml = new PlayerMoveMessage
            {
                GameId = turnStartMessage.GameId,
                Move = move.Name
            }.ToXml();

            Connection.Send(new Message(from, messageXml));

            base.ProcessTurnStartMessage(from, turnStartMessage);
        }

        protected override void ProcessTurnResultMessage(Jid from, TurnResultMessage turnResultMessage)
        {
            _bots[turnResultMessage.GameId].TurnResult(new TurnData
            {
                GameId = turnResultMessage.GameId,
                Player1Move = MoveFactory.GetMove(turnResultMessage.Player1Move),
                Player2Move = MoveFactory.GetMove(turnResultMessage.Player2Move),
            });

            base.ProcessTurnResultMessage(from, turnResultMessage);
        }

        protected override void ProcessGameOverMessage(Jid from, GameOverMessage gameOverMessage)
        {
            _bots[gameOverMessage.GameId].GameOver(gameOverMessage.Winner);
            base.ProcessGameOverMessage(from, gameOverMessage);
        }

        protected override void ProcessTournamentStartedMessage(Jid from, TournamentStartedMessage tournamentStartedMessage)
        {
            _bots = new Dictionary<string, IPlayerBot>();

            foreach (var game in tournamentStartedMessage.Games)
            {
                _bots.Add(game.GameId, _createBotFunc());
            }

            base.ProcessTournamentStartedMessage(from, tournamentStartedMessage);
        }

        protected override void ProcessGameStartMessage(Jid from, GameStartMessage gameStartMessage)
        {
            _bots[gameStartMessage.GameId].GameStart(new GameStartInfo
            {
                GameId = gameStartMessage.GameId,
                MaxMoves = gameStartMessage.MaxMoves,
                Player1 = gameStartMessage.Player1,
                Player2 = gameStartMessage.Player2,
                AllowBubbles = gameStartMessage.AllowBubbles,
                DynamiteCount = gameStartMessage.DynamiteCount
            });

            base.ProcessGameStartMessage(from, gameStartMessage);
        }
    }
}
