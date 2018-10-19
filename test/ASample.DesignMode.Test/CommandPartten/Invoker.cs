using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.CommandPartten
{
    /// <summary>
    /// Asks the command to carry out the action.
    /// </summary>
    public class Invoker
    {
        public ICommand Command { get; set; }

        public void ExecuteCommand()
        {
            Command.Excute();
        }
    }
}
