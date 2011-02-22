using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace RPS.Common
{
    public static class Extensions
    {
        public static string ToXml(this object obj)
        {
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractSerializer(obj.GetType());
                ser.WriteObject(ms, obj);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
