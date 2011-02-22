using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public class Paper : MoveBase
    {
        public override string Name
        {
            get { return MoveNames.Paper; }
        }
    }
}
