using NginxDotnetParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NginxDotnetParserTests
{
    internal class TestHelper
    {

        internal static string TestAssetsDir
        {
            get
            {
                var currentDir = AppDomain.CurrentDomain.BaseDirectory;
                var testAssetsDir = System.IO.Path.Combine(currentDir, "..", "..", "..","..", "TestAssets");
                return testAssetsDir;
            }
        }

        internal static NgxConfig LoadBigConfig()
        {
            var confContent = File.ReadAllText(Path.Combine(TestAssetsDir, "ngx-conf", "big.conf"));
            var ngxConfig = NgxConfig.Read(confContent);

            return ngxConfig;
        }
    }
}
