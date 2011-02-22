using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RPS.Common.Messaging
{
    [DataContract(Name="GameOver", Namespace="http://charlestonaltnet.org/xml/RPS")]
    public class GameOverMessage
    {
        [DataMember]
        public string GameId { get; set; }

        [DataMember]
        public TurnResult Winner { get; set; }
    }
}
