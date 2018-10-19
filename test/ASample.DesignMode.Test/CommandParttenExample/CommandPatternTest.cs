using ASample.DesignMode.Test.Example;
using ASample.DesignMode.Test.Example.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.CommandPartten
{
    [TestClass]
    public class CommandPatternTest
    {
        [TestMethod]
        public void ClientTest()
        {
            var client = new Client();
            client.RunCommand();
        }

        [TestMethod]
        public void LightTest()
        {
            var invoker = new LightInvoker();
            var light = new Light();
            var command = new UpCommand(light);
            invoker.StoreAndExecute (command);
        }
    }
}
