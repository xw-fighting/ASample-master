using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASample.ThreadDemo.Test
{
    public static class BackAndFrontThreadDemo
    {
        public static void BackThread(object obj)
        {
            int i = Convert.ToInt32(obj);
            Console.WriteLine("后台线程开始,i={0}", i);
            Thread.Sleep(5000);
            i++;
            Console.WriteLine("后台线程结束,i={0}", i);
        }

        public static void FrontThread(object obj)
        {
            int i = Convert.ToInt32(obj);
            Console.WriteLine("前台线程开始,i={0}", i);
            Thread.Sleep(5000);
            i++;
            Console.WriteLine("前台线程结束,i={0}", i);
        }
    }
}
