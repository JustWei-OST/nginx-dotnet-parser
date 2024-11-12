using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NginxDotnetParser
{
    public class NgxBlock : NgxAbstractEntry
    {

        private readonly List<NgxAbstractEntry> _children = [];

        public void AddEntry(NgxAbstractEntry entry)
        {
            entry.Parent = this;
            _children.Add(entry);
        }

        public List<NgxAbstractEntry> Children => _children;

        public T? Find<T>(params string[] pars) where T : NgxAbstractEntry
        {
            var all = FindAll(pars);

            var first = all.FirstOrDefault();
            if (first is null)
            {
                return default;
            }
            else
            {
                return (T)first;
            }

        }

        public NgxBlock FindBlock(params string[] pars)
        {
            NgxAbstractEntry? entry = Find<NgxBlock>(pars);

            return (NgxBlock)entry;
        }

        public NgxParam FindParam(params string[] pars)
        {
            NgxAbstractEntry? entry = Find<NgxParam>(pars);

            return (NgxParam)entry;
        }

        public List<NgxAbstractEntry> FindAll(string par)
        {
            var res = new List<NgxAbstractEntry>();
            foreach (var entry in _children)
            {
                var name = entry.GetName();
                if (name is not null)
                {
                    if (name.Equals(par))
                    {
                        res.Add(entry);
                    }
                    switch (entry)
                    {
                        case NgxParam:
                            break;
                        case NgxBlock block:
                            var blockEntries = block.FindAll(par);
                            if (blockEntries.Any())
                            {
                                res.AddRange(blockEntries);
                            }
                            break;
                        case NgxComment:
                            break;
                    }
                }
            }

            return res;

        }

        public List<NgxAbstractEntry> FindAll(params string[] pars)
        {
            var res = new List<NgxAbstractEntry>();

            if (!pars.Any())
            {
                return res;
            }

            string head = pars[0];
            string[]? tail = pars.Count() > 1 ? pars[1..] : [];

            foreach (var entry in _children)
            {
                var entryName = entry.GetName();
                if (entryName is null || !entryName.Equals(head))
                {
                    continue;
                }

                switch (entry)
                {
                    case NgxParam ngxParam:
                        res.Add(ngxParam);
                        break;
                    case NgxBlock block:
                        if (tail.Count() > 0)
                        {
                            var blockEntries = block.FindAll(tail);

                            res.AddRange(blockEntries);
                        }
                        else
                        {
                            res.Add(block);
                        }
                        break;
                    case NgxComment:
                        break;
                }
            }

            return res;
        }

        public virtual string GetParameters()
        {
            string ret = string.Empty;

            //从第二项开始追加
            if (_tokens.Count > 1)
            {
                var tokenBuilder = new StringBuilder();

                for (int i = 1; i < _tokens.Count; i++)
                {
                    tokenBuilder.Append(_tokens[i].Token).Append(" ");
                }

                var result = tokenBuilder.ToString();
                ret = result[..^1];
            }

            return ret;
        }

        public override string Dump()
        {
            var sb = new StringBuilder();
            var blockName = GetName();
            var _parameters = GetParameters();

            sb.Append(blockName);
            sb.Append(" ");
            if (!string.IsNullOrEmpty(_parameters))
            {
                sb.Append(_parameters);
                sb.Append(" ");
            }
            sb.Append("{");
            sb.Append(Environment.NewLine);

            var _innerText = IndentLines(GetInnerText());

            sb.Append(_innerText);
            sb.Append("}");

            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        public string GetInnerText()
        {
            var sb = new StringBuilder();
            //从第二项开始,第一顶是名称
            if (_children.Any())
            {
                for (int i = 0; i < _children.Count; i++)
                {
                    var item = _children[i];
                    sb.Append(item.Dump());
                }
            }
            return sb.ToString();
        }
    }
}
