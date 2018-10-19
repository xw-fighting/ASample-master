using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.DesignMode.Test.CommandPartten
{
    /// <summary>
    /// This is an interface which specifies the Execute operation.
    /// </summary>
    public interface ICommand
    {
        void Excute();
    }
}
