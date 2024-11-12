using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser
{
    public class NginxListenerImpl : NginxBaseListener
    {
        private NgxConfig _result;

        public NgxConfig Result => _result;

        public override void EnterConfig([NotNull] NginxParser.ConfigContext context)
        {
            _result = context.ret;
        }
    }
}
