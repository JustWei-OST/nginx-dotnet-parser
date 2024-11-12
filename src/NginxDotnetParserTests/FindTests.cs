using Microsoft.VisualStudio.TestTools.UnitTesting;
using NginxDotnetParser;
using NginxDotnetParserTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser.Tests
{
    [TestClass()]
    public class FindTests
    {
        [TestMethod()]
        public void FindParamTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var workers = conf.FindParam("worker_processes");
            var value = workers.GetValue();

            Assert.AreEqual("auto", value);


            var saverName = conf.FindParam("http", "server", "server_name");  
            var saverNameValue = saverName.GetValue();

            Assert.AreEqual("_", saverNameValue);

        }

        [TestMethod()]
        public void FindAllTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var rtmpServers = conf.FindAll("http", "upstream", "server");

            Assert.AreEqual(8, rtmpServers.Count);

        }
    }
}

