using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.CommandPartten
{
    /// <summary>
    /// This is a class that performs the Action associated with the request.
    /// </summary>
    public class Receiver
    {
        public void Action(string message)
        {
            Console.WriteLine("Action called with message: {0}", message);
        }
    }
}
