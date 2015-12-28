using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// XPath帮助类
    /// </summary>
    public class XPathHelper
    {
        /// <summary>
        /// 根据XPATH获取筛选的字符串
        /// </summary>
        /// <param name="content">需要提取HTML的内容</param>
        /// <param name="xpath">XPath表达式</param>
        /// <param name="separ">分隔符</param>
        /// <returns>提取后的内容</returns>
        public static string GetStrByXPath(string content, string xpath, string separ)
        {
            var text = "";
            var doc1 = new HtmlDocument();
            doc1.LoadHtml(content);
            var repeatNodes = doc1.DocumentNode.SelectNodes(xpath);
            if (repeatNodes == null)
                return text;
            //循环节点  
            foreach (var node in repeatNodes)
            {
                //text += node.InnerText + separ;
                text += node.InnerHtml + separ;
            }

            return text;
        }
        /// <summary>
        /// 根据XPATH获取筛选的字符串 每个字符串加上前缀后缀
        /// </summary>
        /// <param name="content">需要提取HTML的内容</param>
        /// <param name="xpath">XPath表达式</param>
        /// <param name="preSepar">前缀</param>
        /// <param name="lastSepar">后缀</param>
        /// <returns></returns>
        public static string GetStrByXPath(string content, string xpath, string preSepar, string lastSepar)
        {
            var text = "";
            var doc1 = new HtmlDocument();
            doc1.LoadHtml(content);
            var repeatNodes = doc1.DocumentNode.SelectNodes(xpath);
            if (repeatNodes == null)
                return text;
            //循环节点  
            foreach (var node in repeatNodes)
            {
                //text += node.InnerText + separ;
                text += preSepar + node.InnerHtml + lastSepar;
            }

            return text;
        }
        /// <summary>
        /// 获取某个xpath取到的元素的指定属性的值
        /// </summary>
        /// <param name="content">原内容</param>
        /// <param name="xpath">XPath表达式</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string GetAttrValueByXPath(string content, string xpath, string attrName)
        {
            var text = "";
            var doc1 = new HtmlDocument();
            doc1.LoadHtml(content);
            var repeatNodes = doc1.DocumentNode.SelectNodes(xpath);
            if (repeatNodes == null)
                return text;
            //循环节点  
            foreach (var node in repeatNodes)
            {
                //text += node.InnerText + separ;
                text += node.Attributes[attrName].Value;
            }

            return text;
        }
        /// <summary>
        /// 根据XPATH获取筛选的字符串 每个字符串加上前缀后缀
        /// </summary>
        /// <param name="content">原内容</param>
        /// <param name="xpath">XPath表达式</param>
        /// <param name="attrName">属性名</param>
        /// <param name="preSepar">前缀</param>
        /// <param name="lastSepar">后缀</param>
        /// <returns></returns>
        public static string GetAttrValueByXPath(string content, string xpath, string attrName, string preSepar, string lastSepar)
        {
            var text = "";
            var doc1 = new HtmlDocument();
            doc1.LoadHtml(content);
            var repeatNodes = doc1.DocumentNode.SelectNodes(xpath);
            if (repeatNodes == null)
                return text;
            //循环节点  
            foreach (var node in repeatNodes)
            {
                //text += node.InnerText + separ;
                text += preSepar + node.Attributes[attrName].Value + lastSepar;
            }

            return text;
        }
    }
}
