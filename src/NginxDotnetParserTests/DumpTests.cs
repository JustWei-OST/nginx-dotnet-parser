using Microsoft.VisualStudio.TestTools.UnitTesting;
using NginxDotnetParser;
using NginxDotnetParserTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser.Tests
{
    [TestClass()]
    public class DumpTests
    {
        [TestMethod()]
        public void DumpConfigTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var dump = conf.Dump();

            Assert.IsNotNull(dump);
            Assert.IsTrue(dump.Length > 1000);

            //File.WriteAllTextAsync("DumpTestOutput.conf", dump);
        }
    }
}

