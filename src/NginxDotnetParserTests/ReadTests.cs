using Microsoft.VisualStudio.TestTools.UnitTesting;
using NginxDotnetParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParserTests
{
    [TestClass()]
    public class ReadTests
    {


        [TestMethod()]
        public void ReadTest()
        {
            var o = TestHelper.LoadBigConfig();

            Assert.IsNotNull(o);
            Assert.IsTrue(o.Children.Count > 0);
        }
    }
}