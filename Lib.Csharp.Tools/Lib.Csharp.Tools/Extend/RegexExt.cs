using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lib.Csharp.Tools.Extend
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public static class RegexExt
    {
        
        #region === 正则处理 ===

        /// <summary>
        /// 从一段html中取出一个url
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string GetUrlFromHtml(this string strHtml)
        {
            var strUrl = GetStrByRegx(strHtml, @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-,]*)?");

            return strUrl;
        }
        /// <summary>
        /// 从字符串中取出与正则匹配的字符串
        /// </summary>
        /// <param name="inputStr">源字符串</param>
        /// <param name="strPattern">正则表达式 如:/\\d{2,20}/</param>
        /// <returns>string</returns>
        public static string GetStrByRegx(this string inputStr, string strPattern)
        {
            var retStr = "";
            try
            {
                MatchCollection mc = Regex.Matches(inputStr, strPattern, RegexOptions.IgnoreCase);
                if (mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        retStr += m.Value;
                    }
                }
            }
            catch
            {
            }
            return retStr;
        }
        /// <summary>
        /// 从字符串中取出与正则匹配的字符串组
        /// </summary>
        /// <param name="inputStr">源字符串</param>
        /// <param name="strPattern">正则表达式 注意要带分组 分组名固定为:"gname" 如: <a id=\"ctl00_M_dtgResumeList(?<gname>.*?).*> </param>
        /// <returns>List-string</returns>
        public static List<string> GetListStrByRegxGroup(this string inputStr, string strPattern)
        {
            var list = new List<string>();

            MatchCollection mc = Regex.Matches(inputStr, strPattern, RegexOptions.IgnoreCase);

            if (mc.Count > 0)
            {
                foreach (Match m in mc)
                {
                    var str = m.Groups["gname"].Value.Trim();
                    if (str.Length > 0)
                    {
                        list.Add(str);
                    }
                }
            }

            return list;
        }
        #endregion

        #region ===去除HTML标记===

        /// <summary>
        /// 替换HTML标记
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string FormatHtml(this string strHtml)
        {
            //删除脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"([rn])[s]+", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "xa1", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "xa2", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "xa3", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "xa9", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&#(d+);", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            strHtml = strHtml.Replace("<", "");
            strHtml = strHtml.Replace(">", "");
            strHtml = strHtml.Replace("rn", "");
            strHtml = strHtml.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return strHtml;
        }

        /// <summary>
        /// 删除脚本
        /// </summary>
        /// <param name="strHtml">输入内容</param>
        /// <returns></returns>
        public static string FormatScript(this string strHtml)
        {
            //删除单行脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除脚本块
            strHtml = Regex.Replace(strHtml, @"<script>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);


            return strHtml;
        }

        /// <summary>
        /// 删除脚本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ClearScript(this string html)
        {
            return Regex.Replace(html, @"<script[\S\s]*?>[\S\s]*?</script>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 去除图片中的style
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static string RemoveImgStyle(this string contents)
        {
            var str = contents;
            try
            {
                //提取内容中的图片的style,去掉
                var imgList = XpathHelper.GetAttrValueListByXPath(str, "//img", "style");
                if (imgList != null && imgList.Count > 0)
                {
                    foreach (var styleItem in imgList)
                    {
                        try
                        {
                            var style = "style=\"" + styleItem + "\"";
                            var style2 = "style='" + styleItem + "'";
                            str = str.Replace(style, "").Replace(style2, "");
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return str;
        }


        public static string ClearCommentary(this string html)
        {
            return Regex.Replace(html, @"<!--[\S\s]*?-->", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string ClearStyle(this string html)
        {
            return Regex.Replace(html, @"<style[\S\s]*?>[\S\s]*?</style>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 去除内容中的a标签
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static string RemoveHref(this string contents)
        {
            var str = contents;
            //str = str.Replace("target=\"_blank\"", "");
            try
            {

                str = Regex.Replace(str, @"<a\s*[^>]*>", "", RegexOptions.IgnoreCase);
                str = Regex.Replace(str, @"</a>", "", RegexOptions.IgnoreCase);


                ////提取内容中的 链接的地址
                //var hrefList = StrHelperUtil.GetListOuterHtmlByXPath(str, "//a");
                //if (hrefList != null && hrefList.Count > 0)
                //{
                //    foreach (var item in hrefList)
                //    {
                //        try
                //        {
                //            var aStr = StrHelperUtil.GetStrByXPath(item, "//a", "");
                //            aStr = StrHelperUtil.FormatHtml(aStr);
                //            //var style = "style=\"" + styleItem + "\"";
                //            //var style2 = "style='" + styleItem + "'";
                //            str = str.Replace(item, aStr);
                //            str = Regex.Replace(str, item, aStr);
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
            return str;
        }
        /// <summary>
        /// 清除注释/**/
        /// </summary>
        public static string RemoveComment(this string s)
        {
            return Regex.Replace(s, @"(?ms)""[^""]*""|//.*?$|/\*.*?\*/", delegate(Match m)
            {
                switch (m.Value.Substring(0, 2))
                {
                    //case "//": return "";
                    case "/*":
                        return "";
                    default:
                        return m.Value;
                }
            });
        }
        #endregion
    }
}
