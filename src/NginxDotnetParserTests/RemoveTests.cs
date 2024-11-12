using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParserTests
{
    [TestClass()]
    public class RemoveTests
    {
        [TestMethod()]
        public void RemoveTest()
        {
            var conf = TestHelper.LoadBigConfig();
            var entry = conf.FindParam("http", "server", "ssl_certificate_key");

            entry.Parent!.Children.Remove(entry);

            Console.WriteLine(entry.Parent!.Dump());

            var entryIndex = entry.Parent!.Children.IndexOf(entry);

            Console.WriteLine(entry.Parent!.Dump());
            Assert.AreEqual(-1, entryIndex);

        }
    }
}
