using Microsoft.VisualStudio.TestTools.UnitTesting;
using NginxDotnetParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NginxDotnetParserTests
{
    [TestClass()]
    public class MoveTests
    {
        [TestMethod()]
        public void MoveToTopTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");

            Console.WriteLine(entry.Parent!.Dump());

            entry.MoveToTop();

            Console.WriteLine(entry.Parent.Dump());
            var entryIndex = entry.Parent!.Children.IndexOf(entry);

            Assert.AreEqual(0, entryIndex);
        }

        [TestMethod()]
        public void MoveToBottomTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");

            Console.WriteLine(entry.Parent!.Dump());
            entry.MoveToBottom();
            Console.WriteLine(entry.Parent.Dump());
            var entryIndex = entry.Parent!.Children.IndexOf(entry);

            Assert.AreEqual(6, entryIndex);
        }

        [TestMethod()]
        public void MoveBeforeTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");
            var target = conf.FindParam("http", "server", "server_name");
            Console.WriteLine(entry.Parent!.Dump());
            entry.MoveBefore(target);
            Console.WriteLine(entry.Parent.Dump());
            var entryIndex = entry.Parent!.Children.IndexOf(entry);
            Assert.AreEqual(0, entryIndex);

        }

        [TestMethod()]
        public void MoveAfterTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");
            var target = conf.FindParam("http", "server", "server_name");
            Console.WriteLine(entry.Parent!.Dump());
            entry.MoveAfter(target);
            Console.WriteLine(entry.Parent.Dump());
            var entryIndex = entry.Parent!.Children.IndexOf(entry);
            Assert.AreEqual(1, entryIndex);

        }

        [TestMethod()]
        public void MoveToIndexTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");
            Console.WriteLine(entry.Parent!.Dump());
            entry.MoveToIndex(3);
            Console.WriteLine(entry.Parent.Dump());
            var entryIndex = entry.Parent!.Children.IndexOf(entry);
            Assert.AreEqual(3, entryIndex);

        }

        [TestMethod()]
        public void MoveToIndexTest1()
        {
            //移动到指定NgxBlock的指定位置
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");
            var oldParent = entry.Parent;
            var parent = conf.FindBlock("http", "upstream");

            Console.WriteLine(entry.Parent!.Dump());
            Console.WriteLine(parent!.Dump());

            entry.MoveToIndex(3, parent);

            Console.WriteLine(oldParent.Dump());
            Console.WriteLine(parent!.Dump());

            var entryIndex = entry.Parent!.Children.IndexOf(entry);
            Assert.AreEqual(2, entryIndex);
        }
    }
}