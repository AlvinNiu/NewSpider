using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewSpider
{
    /*
     此处取名RPage，表示的是R 表示Request与Response的首字母，请求与回应使用同一个类
     page,表示页面。每次请求当做一个页面去处理
         */
    /// <summary>表示请求与回应的信息
    /// </summary>
    public class RPage
    {
        /// <summary>请求地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>请求方式
        /// </summary>
        public RMethod Method { get; set; }
        /// <summary>Post请求方式请求的数据
        /// </summary>
        public string PostData { get; set; }
        /// <summary>编码格式
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>请求返回的内容
        /// </summary>
        public string ResponseHtml { get; set; }
        /// <summary>请求是否有错
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>请求的错误信息
        /// </summary>
        public string ErrorInfo { get; set; }
        /// <summary>包含的请求头部信息
        /// </summary>
        public RequestHeader RH { get; set; }
        /// <summary>表示请求的Cookie
        /// </summary>
        public CookieContainer Cookie { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>设置默认值
        /// </summary>
        public RPage()
        {
            Method = RMethod.GET;
            Encoding = Encoding.UTF8;
            RH = new NewSpider.RequestHeader()
            {
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                AcceptCharset = "",
                AcceptEncoding = "",
                AcceptLanguage = "zh-CN,zh;q=0.8",
                CacheControl = "max-age=0",
                Connection = "keep-alive",
                ContentType = "",
                Host = "",
                Referer = "",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"
            };
        }
        /// <summary>请求
        /// </summary>
        public void Start()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.Url);
            request.Accept = this.RH.Accept;
            //request.Connection = this.RH.Connection;
            request.ContentType = this.RH.ContentType;
            request.CookieContainer = this.Cookie;
            if (!string.IsNullOrWhiteSpace(this.RH.Host))
            {
                request.Host = this.RH.Host;
            }
            request.KeepAlive = this.RH.Connection == "keep-alive";
            request.Method = GetMethod();
            if (this.Method==RMethod.POST)
            {
                byte[] data = Encoding.UTF8.GetBytes(this.PostData);
                request.ContentLength = data.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            request.Referer = this.RH.Referer;
            request.UserAgent = this.RH.UserAgent;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                this.StatusCode = response.StatusCode;
                if (this.Cookie == null)
                {
                    this.Cookie = new CookieContainer();
                }
                this.Cookie.Add(response.Cookies);
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream,this.Encoding);
                this.ResponseHtml = sr.ReadToEnd();
            }
        }
        private string GetMethod()
        {
            switch (this.Method)
            {
                case RMethod.GET:
                    return "GET";
                case RMethod.POST:
                    return "POST";
                default:
                    return "GET";
            }
        }
    }
    /// <summary>表示请求头部
    /// </summary>
    public class RequestHeader
    {
        /// <summary>表示客户端能够接受的内容类型
        /// </summary>
        public string Accept { get; set; }
        /// <summary>请求报头域主要用于指定被请求资源的Internet主机和端口号
        /// </summary>
        public string Host { get; set; }
        /// <summary>为服务器提供上下文信息
        /// </summary>
        public string Referer { get; set; }
        /// <summary>表示包含发送请求的用户信息
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>表示浏览器可以接受的字符编码集
        /// </summary>
        public string AcceptCharset { get; set; }
        /// <summary>指定浏览器可以支持的web服务器返回内容压缩编码类型
        /// </summary>
        public string AcceptEncoding { get; set; }
        /// <summary>指定浏览器可以接受的语言。常用的 en,zh
        /// </summary>
        public string AcceptLanguage { get; set; }
        /// <summary>是否保持持久连接。close：表示不保持持久连接，keep-alive：保持持久连接
        /// </summary>
        public string Connection { get; set; }
        /// <summary>请求实体的MIME类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>指定请求应遵循的缓存机制
        /// </summary>
        public string CacheControl { get; set; }
    }

    public enum RMethod
    {
        GET = 1, POST = 2,
    }
}
