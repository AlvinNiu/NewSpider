using NewSpider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSpiderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RPage page = NewSpiderFun.Get("http://www.cnblogs.com");
            Console.ReadKey();
        }
    }
}
