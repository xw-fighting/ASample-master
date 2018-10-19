using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.ProxyPattern
{
    public class RealSubject : ISubject
    {
        public void PerformAction()
        {
            Console.WriteLine("RealSubject action performed.");
        }
    }
}
