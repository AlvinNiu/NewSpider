using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSpider
{
    /*
     此类用于读取自定义的配置文件，配置文件格式如下：

  <NewSpider>
    <tools>
      <add key ="IsLog" value="false"></add>
      <add key ="Path" value="\\Log"></add>
    </tools>
    <urls>
      <addUrl key="google" url="http://www.google.com"  useproxy="true" uselocal="false" proxytype="1"/>
      <addUrl key="sina" url="http://www.sina.com"   useproxy="true" uselocal="false" proxytype="1" />
      <addUrl key="360buys" url="http://www.360buys.com"  useproxy="true" uselocal="false" proxytype="1" />
    </urls>
  </NewSpider>

         */
    public class NewSpiderConfig : ConfigurationSection
    {
        /// <summary>获取配置文件总自定义的配置节
        /// </summary>
        private static NewSpiderConfig setting;
        /// <summary>获取配置文件总自定义的配置节
        /// </summary>
        public static NewSpiderConfig Setting
        {
            get
            {
                if (setting == null)
                    setting = (NewSpiderConfig)ConfigurationManager.GetSection("NewSpider");
                return setting;
            }
        }
        /// <summary>配置子配置节tools
        /// </summary>
        [ConfigurationProperty("tools")]
        [ConfigurationCollection(typeof(UrlCollection), AddItemName = "add")]
        public ToolCollection ToolElements
        {
            get { return (ToolCollection)this["tools"]; }
            set { this["tools"] = value; }
        }
        /// <summary>配置子配置节urls
        /// </summary>
        [ConfigurationProperty("urls")]
        [ConfigurationCollection(typeof(UrlCollection), AddItemName = "add")]
        public UrlCollection UrlElements
        {
            get { return (UrlCollection)this["urls"]; }
            set { this["urls"] = value; }
        }

    }
    /// <summary>需要配置的配置节集合
    /// </summary>
    public class ToolCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ToolElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ToolElement)element).Key;
        }
        /// <summary>获取所有的配置节，并放进字典中
        /// </summary>
        /// <returns></returns>
        public new IDictionary<string,ToolElement> GetEnumerator()
        {
            IDictionary<string,ToolElement> dic = new Dictionary<string, ToolElement>();
            foreach (var key in this.BaseGetAllKeys())
            {
                dic.Add(key.ToString(),(ToolElement)BaseGet(key));
            }
            return dic;
        }
    }
    /// <summary>需要配置的配置节
    /// </summary>
    public class ToolElement:ConfigurationElement
    {
        /// <summary>配置需要配置的键
        /// </summary>
        [ConfigurationProperty("key", DefaultValue = "path", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }
        /// <summary>配置需要配置的值
        /// </summary>
        [ConfigurationProperty("value", DefaultValue = "\\Log", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
    /// <summary>需要配置代理的配置节集合
    /// </summary>
    public class UrlCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new UrlElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UrlElement)element).Key;
        }
        /// <summary>获取配置节的集合
        /// </summary>
        /// <returns></returns>
        public new IList<UrlElement> GetEnumerator()
        {
            IList<UrlElement> list = new List<UrlElement>();
            foreach (var key in this.BaseGetAllKeys())
            {
                list.Add((UrlElement)BaseGet(key));
            }
            return list;
        }
    }
    /// <summary>配置文件中配置需要代理的URL
    /// </summary>
    public class UrlElement : ConfigurationElement
    {
        /// <summary>配置需要代理的地址
        /// </summary>
        [ConfigurationProperty("url", DefaultValue = "www.google.com", IsRequired = true)]
        public string Url
        {
            get { return this["url"] as string; }
            set { this["url"] = value; }
        }
        /// <summary>配置需要代理的键
        /// </summary>
        [ConfigurationProperty("key", DefaultValue = "google", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }
        /// <summary>配置是否使用代理
        /// </summary>
        [ConfigurationProperty("useproxy", DefaultValue = "false", IsRequired = true)]
        public string UserProxy
        {
            get { return (string)this["useproxy"]; }
            set { this["useproxy"] = value; }
        }
        /// <summary>配置在代理失效之后，是否使用本地访问
        /// </summary>
        [ConfigurationProperty("uselocal", DefaultValue = "true", IsRequired = true)]
        public string UserLocal
        {
            get { return (string)this["uselocal"]; }
            set { this["uselocal"] = value; }
        }
        /// <summary>配置代理的类型
        /// </summary>
        [ConfigurationProperty("proxytype", DefaultValue = "1", IsRequired = true)]
        public string ProxyType
        {
            get { return (string)this["proxytype"]; }
            set { this["proxytype"] = value; }
        }
    }
}
