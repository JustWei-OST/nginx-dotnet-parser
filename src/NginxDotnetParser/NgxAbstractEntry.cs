using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using NginxDotnetParser.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NginxDotnetParser
{
    public abstract class NgxAbstractEntry
    {
        public readonly string Id = Guid.NewGuid().ToString("n");
        internal readonly List<NgxToken> _tokens = [];

        public NgxBlock? Parent { get; set; }

        public abstract string Dump();

        public string? GetName() => _tokens.FirstOrDefault()?.ToString();

        public List<NgxToken> GetTokens() => _tokens;

        public void AddValue(NgxToken token) => _tokens.Add(token);

        public void AddValue(string value) => AddValue(new NgxToken(value));

        public void UpdateValue(NgxToken token)
        {
            _tokens.Clear();
            _tokens.Add(token);
        }

        public void UpdateValue(string value) => UpdateValue(new NgxToken(value));


        public virtual List<string> GetValues()
        {
            var values = new List<string>();

            var tokens = GetTokens();
            if (tokens.Count < 2)
                return values;

            // 第一项是名称，所以从第二项开始
            for (int i = 1; i < tokens.Count; i++)
            {
                values.Add(tokens[i].ToString());
            }
            return values;
        }

        public virtual string GetValue()
        {
            return string.Join(" ", GetValues());
        }

        /// <summary>
        /// 缩进每一行
        /// </summary>
        /// <param name="value">要缩进的文本</param>
        /// <returns></returns>
        public string IndentLines(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            else
            {
                //缩进每一行
                var lines = value.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
                var sb = new StringBuilder();
                foreach (var line in lines)
                {
                    //sb.Append("|");
                    sb.Append("    ");
                    sb.Append(line);
                    //sb.Append("#??");
                    sb.Append(Environment.NewLine);
                }
                return sb.ToString();

            }
        }
    }
}