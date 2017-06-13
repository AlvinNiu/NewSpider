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
            //RPage page = NewSpiderFun.Get("http://www.cnblogs.com");
            Common.WriteLog("日志测试");
            //Console.WriteLine("--------------------------------------------------");

            Console.ReadKey();
        }

        static void DIYConfig()
        {
            //Console.WriteLine("----CustomConfigurationSecond---------------------");
            //NewSpiderConfig settingSecond = NewSpiderConfig.Setting;
            //foreach (var item in settingSecond.UrlElements.GetEnumerator())
            //{
            //    Console.WriteLine("settingSecond.UrlElement.Key:" + item.Key);
            //    Console.WriteLine("settingSecond.UrlElement.Url:" + item.Url);
            //    Console.WriteLine("settingSecond.UrlElement.UserLocal:" + item.UserLocal);
            //    Console.WriteLine("settingSecond.UrlElement.UserProxy:" + item.UserProxy);
            //    Console.WriteLine("settingSecond.UrlElement.ProxyType:" + item.ProxyType);

            //}
            //Console.WriteLine("--------------------------------------------------");
            //foreach (var item in settingSecond.ToolElements.GetEnumerator())
            //{
            //    Console.WriteLine("settingSecond.ToolElements.Key:" + item.Key);
            //    Console.WriteLine("settingSecond.ToolElements.Value:" + item.Value.Value);
            //}
        }
    }
}
