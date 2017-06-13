using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSpider
{
    public static class Common
    {
        /*
         此处处理日志，在日志使用时
             */
        public static void WriteLog(string logContent)
        {
            try
            {


                NewSpiderConfig config = NewSpiderConfig.Setting;
                //记录日志
                if (config != null)
                {
                    //判断配置文件中是否有相关的配置节，如果没有则按照默认的走
                    Dictionary<string, ToolElement> toolDic = config.ToolElements.GetEnumerator() as Dictionary<string, ToolElement>;
                    //检查配置文件中是否有IsLog配置节，并且是否为false
                    if (!toolDic.ContainsKey("IsLog") || toolDic["IsLog"].Value.Equals("true"))
                    {//在这种情况下，可以写日志
                        string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Log";//默认的日志路径
                        if (toolDic.ContainsKey("Path") && !string.IsNullOrWhiteSpace(toolDic["Path"].Value))
                        {
                            //此时可以按照配置文件中配置的路径写日志

                            //判断路径是绝对路径还是相对路径
                            if (toolDic["Path"].Value.Substring(0, 1).Equals("/"))
                            {
                                path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + toolDic["Path"].Value;
                            }
                            else
                            {
                                path = toolDic["Path"].Value;
                            }
                        }
                        WriteLog(path, "NewSpider" + DateTime.Now.ToString("yyyyMMdd") + ".txt", logContent);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Log", "NewSpider" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "原异常信息："+logContent+"--------------写日志方法出现异常，"+e.ToString());
            }
        }
        private static object loglock = new object();
        //写日志方法，logFileName:文件名称 ，logContent:日志内容
        private static void WriteLog(string path, string filename, string logContent)
        {
            lock (loglock)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter sw = File.AppendText(path + "/" + filename))
                {
                    if (!string.IsNullOrWhiteSpace(logContent))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + logContent);
                        sw.WriteLine(sb.ToString());
                    }
                }
            }
        }
    }
}
