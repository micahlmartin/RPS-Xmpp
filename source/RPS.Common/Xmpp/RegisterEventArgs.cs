using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;

namespace RPS.Common.Xmpp
{
    public class RegisterEventArgs : EventArgs
    {
        public RegisterEventArgs(Jid user)
        {
            User = user;
        }

        public Jid User { get; set; }
    }
}
