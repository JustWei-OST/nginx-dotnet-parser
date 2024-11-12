using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using NginxDotnetParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NginxDotnetParser
{
    public class NgxConfig : NgxBlock
    {
        public static NgxConfig Read(string confContent)
        {
            var input = new AntlrInputStream(confContent);
            var lexer = new NginxLexer(input);

            var tokens = new CommonTokenStream(lexer);
            var parser = new NginxParser(tokens);
            var walker = new ParseTreeWalker();

            IParseTree tree = parser.config();
            var listener = new NginxListenerImpl();
            walker.Walk(listener, tree);

            var ret = listener.Result;

            return ret;
        }

        //将多个配置内容合并成一个配置

        public static NgxConfig Read(string mainConfContent, Dictionary<string, List<string>> includeContents)
        {
            var ngxConfig = Read(mainConfContent);

            var includeNgxConfigs = new Dictionary<string, List<NgxConfig>>();

            foreach (var kv in includeContents)
            {
                var ngxConfigs = kv.Value.Select(Read).ToList();
                includeNgxConfigs.Add(kv.Key, ngxConfigs);
            }

            var retConfig = ngxConfig.MergeIncludes(includeNgxConfigs);

            return retConfig;
        }

        public override string Dump() => $"{GetInnerText()}{Environment.NewLine}";
    }
}
