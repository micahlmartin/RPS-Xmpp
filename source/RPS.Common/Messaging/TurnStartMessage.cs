using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RPS.Common.Messaging
{
    [DataContract(Name = "TurnStart", Namespace = "http://charlestonaltnet.org/xml/RPS")]
    public class TurnStartMessage
    {
        [DataMember]
        public string GameId { get; set; }
    }
}
