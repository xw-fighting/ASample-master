using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASample.ThreadDemo.Test
{
    class Program
    {
        private static int i = 0;
        static void Main(string[] args)
        {
            //var backAndFrontThreadDemo = new BackAndFrontThreadDemo();
            Console.WriteLine("主线程开始");
            i++;
            //IsBackground=true，将其设置为后台线程
            Thread t = new Thread(new ParameterizedThreadStart(BackAndFrontThreadDemo.BackThread)) { IsBackground = true };
            t.Start();

            Thread t2 = new Thread(new ParameterizedThreadStart(BackAndFrontThreadDemo.FrontThread)) { IsBackground = false };
            t2.Start();

            Console.WriteLine("主线程在做其他的事！");
            //主线程结束，后台线程会自动结束，不管有没有执行完成
            //Thread.Sleep(300);

            //Thread.Sleep(1500);
            Console.WriteLine("主线程结束,i={0}",i);
            Console.ReadKey();
        }

        private static void Run()
        {

            Thread.Sleep(3000);
            i++;
            Console.WriteLine("后台子线程开始,i={0}",i);

        }
    }
}
