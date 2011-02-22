using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RPS.Common.Messaging
{
    [DataContract(Name = "TournamentStarted", Namespace = "http://charlestonaltnet.org/xml/RPS")]
    public class TournamentStartedMessage
    {
        [DataMember(Name="Games")]
        public GameStartMessage[] Games { get; set; }
    }
}
