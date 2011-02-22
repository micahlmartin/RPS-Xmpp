using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RPS.Common.Messaging
{
    [DataContract(Name = "GameStart", Namespace = "http://charlestonaltnet.org/xml/RPS")]
    public class GameStartMessage
    {
        [DataMember]
        public string GameId { get; set; }

        [DataMember]
        public int MaxMoves { get; set; }

        [DataMember]
        public string Player1 { get; set; }

        [DataMember]
        public string Player2 { get; set; }

        [DataMember]
        public int DynamiteCount { get; set; }

        [DataMember]
        public bool AllowBubbles { get; set; }
    }
}
