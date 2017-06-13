using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewSpider
{
    /*
     表示请求的方法，包括get与post，以及自定义请求的参数
         */
    public static class NewSpiderFun
    {

        #region Get请求

        /// <summary>最基本的Get请求，其他的请求参数，全部是默认的 编码方式默认为utf-8
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns></returns>
        public static RPage Get(string url)
        {
            return Get(url, Encoding.UTF8, null);
        }
        /// <summary>Get请求，可以设置编码方式
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static RPage Get(string url, Encoding encoding)
        {
            return Get(url, encoding, null);
        }
        /// <summary>Get请求，携带Cookie进行访问
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="cookie">携带的cookie</param>
        /// <returns></returns>
        public static RPage Get(string url, CookieContainer cookie)
        {
            return Get(url, Encoding.UTF8, cookie);
        }
        /// <summary>Get请求，可以设置编码方式，以及携带Cookie
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="cookie">携带的cookie</param>
        /// <returns></returns>
        public static RPage Get(string url, Encoding encoding, CookieContainer cookie)
        {
            RPage page = new RPage();
            page.Url = url;
            page.Method = RMethod.GET;
            page.Encoding = encoding;
            page.Cookie = cookie;
            return SpiderPage(page);
        }

        #endregion


        #region Post 请求

        /// <summary>最基本的Post请求，其他的请求参数，全部是默认的,编码方式默认为utf-8
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="postData">请求的数据</param>
        /// <returns></returns>
        public static RPage Post(string url, string postData)
        {
            return Post(url, postData, Encoding.UTF8, null);
        }
        /// <summary>post请求，设置编码方式
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="postData">请求的数据</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static RPage Post(string url, string postData, Encoding encoding)
        {
            return Post(url, postData, encoding, null);
        }
        /// <summary>post请求，携带cookie
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="postData">请求的数据</param>
        /// <param name="cookie">携带的cookie</param>
        /// <returns></returns>
        public static RPage Post(string url, string postData, CookieContainer cookie)
        {
            return Post(url, postData, Encoding.UTF8, cookie);
        }
        /// <summary>Post 请求，可以设置编码方式，以及携带Cookie
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="postData">请求的数据</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="cookie">携带的cookie</param>
        /// <returns></returns>
        public static RPage Post(string url, string postData, Encoding encoding, CookieContainer cookie)
        {
            RPage page = new RPage();
            page.Url = url;
            page.Encoding = encoding;
            page.Cookie = cookie;
            page.Method = RMethod.POST;
            page.PostData = postData;
            return SpiderPage(page);
        }
        #endregion
        /// <summary>使用自定义的参数请求
        /// </summary>
        /// <param name="page">请求的参数对象</param>
        /// <returns></returns>
        public static RPage SpiderPage(RPage page)
        {
            try
            {
                page.Start();
            }
            catch (WebException webEx)
            {
                WriteLog(string.Format("SpiderPage Url={0},postData={1},Error={2}", page.Url, page.PostData, webEx.Message));
                page.IsError = true;
                page.ErrorInfo = webEx.Message;
            }
            catch (Exception e)
            {
                WriteLog(string.Format("SpiderPage Url={0},postData={1},Error={2}", page.Url, page.PostData, e.Message));
                page.IsError = true;
                page.ErrorInfo = e.Message;
            }
            return page;
        }
        private static void WriteLog(string logContent)
        {
            Common.WriteLog("NewSpider NewSpiderFun " + logContent);
        }
    }
}
