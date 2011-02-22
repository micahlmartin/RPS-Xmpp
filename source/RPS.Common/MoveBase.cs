using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPS.Common
{
    public abstract class MoveBase
    {
        public abstract string Name { get; }

        public override bool Equals(object obj)
        {
            var mvObj = obj as MoveBase;

            if(mvObj == null) return false;

            return Name == mvObj.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
