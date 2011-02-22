using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace RPS.Common.Messaging
{
    public static class MessageFactory
    {
        public static object GetMessageFromXml(string xml)
        {   
            try
            {
                var xMsg = XElement.Parse(xml);
                var ns = xMsg.GetDefaultNamespace();


                var name = xMsg.Name;

                if (name == ns + "GameStart")
                    return DeserializeMessage<GameStartMessage>(xml);
                if (name == ns + "GameOver")
                    return DeserializeMessage<GameOverMessage>(xml);
                if (name == ns + "TurnResult")
                    return DeserializeMessage<TurnResultMessage>(xml);
                if (name == ns + "TurnStart")
                    return DeserializeMessage<TurnStartMessage>(xml);
                if (name == ns + "PlayerMove")
                    return DeserializeMessage<PlayerMoveMessage>(xml);
                if (name == ns + "Register")
                    return DeserializeMessage<RegisterMessage>(xml);
                if (name == ns + "RegistrationComplete")
                    return DeserializeMessage<RegistrationCompleteMessage>(xml);
                if (name == ns + "TournamentStarted")
                    return DeserializeMessage<TournamentStartedMessage>(xml);

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private static T DeserializeMessage<T>(string message)
        {
            using (var ms = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(ms))
                {
                    streamWriter.Write(message);
                    streamWriter.Flush();

                    ms.Position = 0;

                    var ser = new DataContractSerializer(typeof(T));
                    return (T)ser.ReadObject(ms);
                }
            }
        }
    }
}
