using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser
{
    public class NgxParam : NgxAbstractEntry
    {
        public override string Dump() => $"{GetName()} {GetValue()};{Environment.NewLine}";
    }
}
