using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.CommandPartten
{
    /// <summary>
    /// This is a class that implements the Execute operation by invoking operation(s) on the Receiver.
    /// </summary>
    public class ConcreteCommand : ICommand
    {
        public ConcreteCommand(Receiver recevier)
        {
            _receiver = recevier ;
        }

        protected Receiver _receiver;

        public  string Parameter { get; set; }
        public void Excute()
        {
            _receiver.Action(Parameter);
        }
    }
}
