using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.CommandPartten
{
    /// <summary>
    /// This is the class that creates and executes the command object.
    /// </summary>
    public class Client
    {
        public void RunCommand()
        {
            Invoker invoker = new Invoker();
            Receiver receiver = new Receiver();
            ConcreteCommand command = new ConcreteCommand(receiver);
            command.Parameter = "Dot Net Tricks !!";
            invoker.Command = command;
            invoker.ExecuteCommand();
        }
    }
}
