using NginxDotnetParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NginxDotnetParser.Extensions
{
    public static class NgxConfigExtensions
    {
        public static List<NgxAbstractEntry> FindAllIncludes(this NgxConfig mainConfig)
        {
            return mainConfig.FindAll("include");
        }

        //合并
        public static NgxConfig MergeIncludes(this NgxConfig mainConfig, Dictionary<string, List<NgxConfig>> includeConfigs)
        {
            throw new NotImplementedException();
        }
    }
}
