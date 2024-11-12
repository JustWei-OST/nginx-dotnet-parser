using Microsoft.VisualStudio.TestTools.UnitTesting;
using NginxDotnetParser;
using NginxDotnetParserTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser.Tests
{
    [TestClass()]
    public class AddTests
    {
        [TestMethod()]
        public void AddValueTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var saverName = conf.FindParam("http", "server", "server_name");
            var saverNameValue = saverName.GetValue();

            Assert.AreEqual("_", saverNameValue);

            saverName.AddValue("newValue");

            saverNameValue = saverName.GetValue();

            Assert.AreEqual("_ newValue", saverNameValue);

        }

        [TestMethod()]
        public void AddParamEntryTest()
        {
            var conf = TestHelper.LoadBigConfig();

            var entry = new NgxParam();
            entry.AddValue("newParam");
            entry.AddValue("newParamValue");

             conf.AddEntry(entry);

            var newEntry = conf.FindParam("newParam");
            var value = newEntry.GetValue();

            Assert.AreEqual("newParamValue", value);
        }

        [TestMethod()]
        public void AddBlockEntryTest()
        {
            var conf = TestHelper.LoadBigConfig();

            var firstHttp = conf.FindAll("http").First() as NgxBlock;

            var newServerBlock = new NgxBlock();
            newServerBlock.AddValue("server");

            var newServerListen = new NgxParam();
            newServerListen.AddValue("listen");
            newServerListen.AddValue("8889");
            newServerBlock.AddEntry(newServerListen);

            firstHttp!.AddEntry(newServerBlock);

            var newServerListenValue = newServerBlock.FindParam("listen").GetValue();

            Assert.AreEqual("8889", newServerListenValue);

        }
    }
}


