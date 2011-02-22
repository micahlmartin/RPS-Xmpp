using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RPS.Common.Messaging
{
    [DataContract(Name = "PlayerMove", Namespace = "http://charlestonaltnet.org/xml/RPS")]
    public class PlayerMoveMessage
    {
        [DataMember]
        public string GameId { get; set; }

        [DataMember]
        public string Move { get; set; }
    }
}
