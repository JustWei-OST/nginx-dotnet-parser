using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser
{
    public class NgxComment(string? v) : NgxAbstractEntry
    {
        private string? v = v; // 包含#号

        public override string Dump() => $"{v}{Environment.NewLine}";
    }
}
