using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser
{
    public class NgxIfBlock : NgxBlock
    {
        public override string GetParameters()
        {
            string ret = string.Empty;

            var tokens = GetTokens();
            //从第二项开始追加
            if (tokens.Count > 1)
            {
                var sb = new StringBuilder()
                    .Append("(");

                for (int i = 1; i < tokens.Count; i++)
                {
                    sb.Append(tokens[i].Token).Append(" ");
                }

                sb.Append(")");

                ret = sb.ToString();
            }
            return ret;
        }

    }
}
