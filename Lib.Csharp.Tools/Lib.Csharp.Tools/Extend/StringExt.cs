using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;
using System.Reflection;

namespace Lib.Csharp.Tools.Extend
{
    public static class StringExt
    {

        #region ===字符串精确截取===

        /// <summary>
        /// 字符串精确截取函数
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <returns>返回截取后的字符串</returns>
        public static string GetStrByLen(this string aSrcStr, int aLimitedNum)
        {

            string temp;
            //var n = aSrcStr.Count(c => c >= 0x4e00 && c <= 0x9fa5);//中文的个数

            if (aSrcStr.Length > aLimitedNum)
            {
                temp = aSrcStr.Substring(0, aLimitedNum - 1 + 50000);
                temp += "..";
            }
            else
            {
                temp = aSrcStr;
            }
            return temp;
        }



        /// <summary>
        /// 字符串精确截取函数2
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <returns>返回截取后的字符串</returns>
        public static string GetStrByLen2(this string aSrcStr, int aLimitedNum)
        {
            if (aSrcStr.Length > aLimitedNum)
            {
                aSrcStr = aSrcStr.Substring(0, aSrcStr.Length - 1);
            }

            if (aLimitedNum <= 0) return aSrcStr;
            var tmpStr = aSrcStr;
            var tmpStrBytes = Encoding.GetEncoding("GB2312").GetBytes(tmpStr);
            if (tmpStrBytes.Length <= aLimitedNum)
            {
                return aSrcStr;
            }
            byte[] lStrBytes = null;
            var needStrNum = 0;
            if (tmpStrBytes[aLimitedNum] > 127)
            {
                lStrBytes = new byte[aLimitedNum + 1];
                needStrNum = aLimitedNum + 1;
            }
            else
            {
                lStrBytes = new byte[aLimitedNum];
                needStrNum = aLimitedNum;
            }
            Array.Copy(tmpStrBytes, lStrBytes, needStrNum);
            var temp = Encoding.GetEncoding("GB2312").GetString(lStrBytes);
            if (temp.Substring(temp.Length - 1, 1) == "?")
                temp = temp.Substring(0, temp.Length - 1) + "";
            return temp + Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(".."));
        }

        /// <summary>
        /// 字符串精确截取函数3
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <returns>返回截取后的字符串</returns>
        public static string GetStrByLen3(this string aSrcStr, int aLimitedNum)
        {
            if (aSrcStr.Length > aLimitedNum)
            {
                aSrcStr = aSrcStr.Substring(0, aSrcStr.Length - 1);
            }

            if (aLimitedNum <= 0) return aSrcStr;
            var tmpStr = aSrcStr;
            byte[] tmpStrBytes = Encoding.GetEncoding("GB2312").GetBytes(tmpStr);
            if (tmpStrBytes.Length <= aLimitedNum)
            {
                return aSrcStr;
            }
            else
            {
                byte[] lStrBytes = null;
                int needStrNum = 0;
                if (tmpStrBytes[aLimitedNum] > 127)
                {
                    lStrBytes = new byte[aLimitedNum + 1];
                    needStrNum = aLimitedNum + 1;
                }
                else
                {
                    lStrBytes = new byte[aLimitedNum];
                    needStrNum = aLimitedNum;
                }
                Array.Copy(tmpStrBytes, lStrBytes, needStrNum);
                string temp = Encoding.GetEncoding("GB2312").GetString(lStrBytes);
                if (temp.Substring(temp.Length - 1, 1) == "?")
                    temp = temp.Substring(0, temp.Length - 1) + "";
                return temp;
            }
        }


        /// <summary>
        /// 字符串精确截取函数4
        /// </summary>
        /// <param name="aSrcStr">要截取的字符串</param>
        /// <param name="aLimitedNum">要截取的长度</param>
        /// <param name="dotCount">.数</param>
        /// <returns></returns>
        public static string GetLenghtStr(this string aSrcStr, int aLimitedNum, int dotCount)
        {
            if (aLimitedNum <= 0) return aSrcStr;
            var tmpStr = aSrcStr;
            byte[] tmpStrBytes = Encoding.GetEncoding("GB2312").GetBytes(tmpStr);
            if (tmpStrBytes.Length <= aLimitedNum)
            {
                return aSrcStr;
            }
            else
            {
                byte[] lStrBytes = null;
                var needStrNum = 0;
                if (tmpStrBytes[aLimitedNum] > 127)
                {
                    lStrBytes = new byte[aLimitedNum + 1];
                    needStrNum = aLimitedNum + 1;
                }
                else
                {
                    lStrBytes = new byte[aLimitedNum];
                    needStrNum = aLimitedNum;
                }
                Array.Copy(tmpStrBytes, lStrBytes, needStrNum - 1);
                string temp = Encoding.GetEncoding("GB2312").GetString(lStrBytes);
                if (temp.Substring(temp.Length - 1, 1) == "?" || temp.Substring(temp.Length - 1, 1) == "？")
                    temp = temp.Substring(0, temp.Length - 1) + "";
                //temp=temp.Replace("?","").Replace("？","");
                string strDot = string.Empty;

                for (var i = 0; i < dotCount; i++)
                {
                    strDot += ".";
                }
                return temp + strDot;
            }
        }
        #endregion

        #region ===过滤不安全字符===

        /// <summary>
        /// 过滤不安全字符
        /// </summary>
        /// <param name="sStr">要过滤的字符串</param>
        /// <returns>过滤的字符串</returns>
        public static string GetSafetyStr(this string sStr)
        {
            string returnStr = sStr;
            returnStr = returnStr.Replace("&", "&amp;");
            returnStr = returnStr.Replace("'", "’");
            returnStr = returnStr.Replace("\"", "&quot;");
            returnStr = returnStr.Replace(" ", "&nbsp;");
            returnStr = returnStr.Replace("<", "&lt;");
            returnStr = returnStr.Replace(">", "&gt;");
            returnStr = returnStr.Replace("\n", "<br>");

            return returnStr;//Globals.GetOutString(returnStr);
        }
        #endregion

        #region ===sql分配过滤===
        /// <summary>
        /// 字符串过滤
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static string FormatSql(this string strsql)
        {
            
            //Regex reg = new Regex(@"/w|and|exec|insert|select|delete|update|count|master|truncate|declare|char|mid|chr|/w", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //5次循环替换
            try
            {
                for (var i = 0; i < 5; i++)
                {
                    var mc = Regex.Matches(strsql, @"/<script|exec |insert |select |delete |update |count |master |truncate |declare |char\(|chr\(|mid\(|<script |</script", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    if (mc.Count > 0)
                    {

                        foreach (Match m in mc)
                        {
                            //string _goldAmend = m.Groups["goldAmend"].Value.ToString().Trim();
                            string str1 = m.Groups[0].Value.ToString();
                            strsql = strsql.Replace(str1, "");
                        }
                    }
                    i += 1;
                }
            }
            catch
            { }

            strsql = strsql.Replace("'", "''");
            return strsql;
        }
        #endregion

        #region ===要输入的文字[换行]===
        public static string ReplaceBr(this string sStr)
        {
            return sStr.Replace("\r", "<br>");
        }
        #endregion

        #region ===给指定的字标上颜色===

        /// <summary>
        /// 给指定的字标上颜色
        /// </summary>
        /// <param name="word">源内容</param>
        /// <param name="str">上色的字符</param>
        /// <param name="color">颜色</param>
        /// <param name="isbold">是否加粗</param>
        /// <returns></returns>
        public static string ReplaceColor(string word, string str, string color, bool isbold)
        {
            return word.Replace(str, (isbold ? "<b>" : "") + "<font color=" + color + ">" + str + "" + (isbold ? "</b>" : ""));
        }
        public static string ReplaceColor(this string word, string str, string color)
        {
            return word.Replace(str, "<font color=" + color + ">" + str + "");
        }
        #endregion

        #region ===查出中文字符串对应的拼音缩写，如“1中国c”返回"1ZGc",如果是非中文，则返回本身===

        /// <summary>
        /// 查出中文字符串对应的拼音缩写，如“1中国c”返回"1ZGc",如果是非中文，则返回本身
        /// </summary>
        /// <param name="chineseStr"></param>
        /// <returns></returns>
        public static string GetCnLetter(this string chineseStr)
        {
            byte[] cBs = Encoding.Default.GetBytes(chineseStr);

            if (cBs.Length < 2)
                return chineseStr;

            byte ucHigh, ucLow;
            int nCode;

            string strFirstLetter = string.Empty;
            for (int i = 0; i < cBs.Length; i++)
            {
                //1个中文字符的的2个字节都必须大于的128，非中文字符返回本身
                if (cBs[i] < 0x80)
                {
                    strFirstLetter += Encoding.Default.GetString(cBs, i, 1);
                    continue;
                }

                ucHigh = (byte)cBs[i];//中文字符的第1个字节
                ucLow = (byte)cBs[i + 1];//中文字符的第2个字节
                //1个中文字符的2个字节都必须大于161，否则是无效中文
                if (ucHigh < 0xa1 || ucLow < 0xa1)
                    continue;
                else
                    // Treat code by section-position as an int type parameter,
                    //获得一个中文字符的Asc码值 so make following change to nCode.
                    nCode = (ucHigh - 0xa0) * 100 + ucLow - 0xa0;

                string str = FirstLetter(nCode);
                strFirstLetter += str != string.Empty ? str : Encoding.Default.GetString(cBs, i, 2);//如果没有查询出该中文字符的字母则返回该中文
                i++;
            }
            return strFirstLetter;
        }

        /// <summary>
        /// Get the first letter of pinyin according to specified Chinese character code
        /// </summary>
        /// <param name="nCode">the code of the chinese character</param>
        /// <returns>receive the string of the first letter</returns>
        public static string FirstLetter(int nCode)
        {
            string strLetter = string.Empty;

            if (nCode >= 1601 && nCode < 1637) strLetter = "A";
            if (nCode >= 1637 && nCode < 1833) strLetter = "B";
            if (nCode >= 1833 && nCode < 2078) strLetter = "C";
            if (nCode >= 2078 && nCode < 2274) strLetter = "D";
            if (nCode >= 2274 && nCode < 2302) strLetter = "E";
            if (nCode >= 2302 && nCode < 2433) strLetter = "F";
            if (nCode >= 2433 && nCode < 2594) strLetter = "G";
            if (nCode >= 2594 && nCode < 2787) strLetter = "H";
            if (nCode >= 2787 && nCode < 3106) strLetter = "J";
            if (nCode >= 3106 && nCode < 3212) strLetter = "K";
            if (nCode >= 3212 && nCode < 3472) strLetter = "L";
            if (nCode >= 3472 && nCode < 3635) strLetter = "M";
            if (nCode >= 3635 && nCode < 3722) strLetter = "N";
            if (nCode >= 3722 && nCode < 3730) strLetter = "O";
            if (nCode >= 3730 && nCode < 3858) strLetter = "P";
            if (nCode >= 3858 && nCode < 4027) strLetter = "Q";
            if (nCode >= 4027 && nCode < 4086) strLetter = "R";
            if (nCode >= 4086 && nCode < 4390) strLetter = "S";
            if (nCode >= 4390 && nCode < 4558) strLetter = "T";
            if (nCode >= 4558 && nCode < 4684) strLetter = "W";
            if (nCode >= 4684 && nCode < 4925) strLetter = "X";
            if (nCode >= 4925 && nCode < 5249) strLetter = "Y";
            if (nCode >= 5249 && nCode < 5590) strLetter = "Z";

            //if (strLetter == string.Empty)
            //    System.Windows.Forms.MessageBox.Show(String.Format("信息：\n{0}为非常用字符编码,不能为您解析相应匹配首字母！",nCode));
            return strLetter;
        }
        #endregion

        #region ===获取用户登录IP===
        /// <summary>
        /// 获取用户登录IP客户端外网ip
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string userIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIp))
            {
                userIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return userIp;
        }

        /// <summary>
        /// 获得当前页面内网的IP
        /// </summary>
        /// <returns>当前页面客户端内网的IP</returns>
        public static string GetIp2()
        {
            string result;
            try
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            catch
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }


            return result;

        }

        public static string IpAddress
        {
            get
            {
                var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(result))
                {
                    //可能有代理 
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIp4Address(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址 
                                }
                            }
                        }
                        else if (IsIp4Address(result)) //代理即是IP格式 
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP 
                    }

                }

                var ipAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];



                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.UserHostAddress;

                return result;
            }
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIp4Address(this string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        #endregion

        #region ===类型转换===
        /// <summary>
        /// 转换成数值型
        /// </summary>
        /// <param name="strData">要转换的字符型数值</param>
        /// <returns></returns>
        public static int ToInt32(this string strData)
        {
            int iData = 0;
            if (strData.Trim() == "")
                iData = 0;
            try
            {
                iData = Convert.ToInt32(strData);
            }
            catch
            {
                iData = 0;
                if (strData.IndexOf(".", StringComparison.Ordinal) > 0)
                {
                    try
                    {
                        iData = Convert.ToInt32(strData.Substring(0, strData.IndexOf(".", StringComparison.Ordinal)));
                    }
                    catch
                    {

                    }
                }
            }
            return iData;
        }
        /// <summary>
        /// 转换成double
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static double ToDouble(this string strData)
        {
            double iData = 0;
            if (strData.Trim() == "")
                iData = 0;
            try
            {
                iData = Convert.ToDouble(strData);
            }
            catch
            {
                iData = 0;

            }
            return iData;
        }


        public static string Base64Decode(this string s)
        {
            return Base64Decode(s, Encoding.UTF8);
        }

        public static string Base64Decode(this string s, Encoding e)
        {
            return e.GetString(Convert.FromBase64String(s));
        }

        public static string Base64Encode(this string s)
        {
            return Base64Encode(s, Encoding.UTF8);
        }

        public static string Base64Encode(this string s, Encoding e)
        {
            return Convert.ToBase64String(e.GetBytes(s));
        }

        /// <summary>
        /// 是否含有Unicode码字符串
        /// </summary>
        public static bool HasUnicode(this string s)
        {
            return s.IsMatch("\\\\u[0-9a-fA-f]{4}");
        }
        /// <summary>
        /// 字符串转为Unicode码字符串
        /// </summary>
        public static string ToUnicode(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var c in s)
            {
                sb.Append("\\u").Append(((int)c).ToString("x").PadLeft(4, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Unicode码字符串转为字符串
        /// </summary>
        public static string ToStringFromUnicode(this string s)
        {
            return Regex.Unescape(s);
        }

        public static bool ToBoolean(this string val, bool d = false)
        {
            bool result;
            if (!bool.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }
        public static DateTime ToDateIssue(this string val)
        {
            if (val.Length < 8)
                return DateTime.MinValue;
            return ToDateTime(val.Substring(0, 8).Insert(4, "-").Insert(7, "-"));
        }

        public static DateTime ToDateTime(this string val, DateTime d)
        {
            DateTime result;
            if (!DateTime.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static DateTime ToDateTime(this string val)
        {
            DateTime result;
            if (!DateTime.TryParse(val, out result))
            {
                result = DateTime.MinValue;
            }
            return result;
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        public static string ToDBC(this string input)
        {
            var chArray = input.ToCharArray();
            for (var i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == '　')
                {
                    chArray[i] = ' ';
                }
                else if ((chArray[i] > 0xff00) && (chArray[i] < 0xff5f))
                {
                    chArray[i] = (char)(chArray[i] - 0xfee0);
                }
            }
            return new string(chArray);
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        public static string ToSBC(this string input)
        {
            var chArray = input.ToCharArray();
            for (var i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    chArray[i] = '　';
                }
                else if (chArray[i] < '\x007f')
                {
                    chArray[i] = (char)(chArray[i] + 0xfee0);
                }
            }
            return new string(chArray);
        }

        public static decimal ToDecimal(this string val)
        {
            var result = 0M;
            decimal.TryParse(val, out result);
            return result;
        }

        public static decimal ToDecimal(this string val, decimal d = 0M)
        {
            decimal result;
            if (!decimal.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static double ToDouble(this string val, double d = 0d)
        {
            double result;
            if (!double.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        /// <summary>
        /// 唯一性的标识符
        /// </summary>
        public static Guid ToGuid(this string val)
        {
            var empty = Guid.Empty;
            if (!string.IsNullOrEmpty(val))
            {
                empty = new Guid(val);
            }
            return empty;
        }

        public static int ToInt32(this string val, int d = 0)
        {
            int result;
            if (!int.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }
        public static long ToInt64(this object val, long d = 0L)
        {
            try
            {
                return Convert.ToInt64(val);
            }
            catch
            {
                return d;
            }
        }
        public static int ToInt32(this object val, int d = 0)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return d;
            }
        }

        public static string ToString(this string val, string d = "")
        {
            var result = d;
            if (!string.IsNullOrEmpty(val))
            {
                result = val;
            }
            return result;
        }

        public static byte ToByte(this string val, byte d = 0)
        {
            byte result;
            if (!byte.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static float ToFloat(this string val, float d = 0F)
        {
            float result;
            if (!float.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static int ToInt32(this char val, int d = 0)
        {
            return ToInt32(val.ToString(), d);
        }

        public static long ToLong(this string val, long d = 0L)
        {
            long result;
            if (!long.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        public static DateTime? ToNullableDateTime(this string val)
        {
            var minValue = DateTime.MinValue;
            if (!DateTime.TryParse(val, out minValue))
            {
                return null;
            }
            if (minValue == DateTime.MinValue)
            {
                return null;
            }
            return minValue;
        }

        public static short ToShort(this string val, short d = (short)0)
        {
            short result;
            if (!short.TryParse(val, out result))
            {
                result = d;
            }
            return result;
        }

        #endregion

        #region ===format===

        /// <summary>
        /// 去掉字符串前后符号
        /// </summary>
        /// <param name="strDh">要去掉的字符</param>
        /// <param name="strFh">符号</param>
        /// <returns></returns>
        public static string FormatDh(string strDh, string strFh)
        {
            if (strDh.Length > 1)
            {
                //strDh = strDh.Replace(",,", ",");
                if (strDh.Substring(0, 1) == strFh)
                    strDh = strDh.Substring(1);
                if (strDh.Substring(strDh.Length - 1) == strFh)
                    strDh = strDh.Substring(0, strDh.Length - 1);
            }
            return strDh;
        }


        /// <summary>
        /// 格式化金额
        /// </summary>
        /// <param name="strJy">转入要转换的字符</param>
        /// <param name="iType">为类型，1表示为425.40元，2表示￥425.40元</param>
        /// <returns></returns>
        public static string FormatMoney(this string strJy, int iType)
        {
            if (strJy.Trim() == "")
                return "";

            var str = "";
            try
            {
                double dNum = Convert.ToDouble(strJy);
                if (iType == 1)
                {
                    str = dNum.ToString("N");
                }
                else if (iType == 2)
                {
                    str = dNum.ToString("C");
                }
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// 转换成性别
        /// </summary>
        /// <param name="strSex">要转换的字符，如1表示男，2表示女，0表示性别不限</param>
        /// <returns></returns>
        public static string FormatSex(this string strSex)
        {
            if (strSex.Trim() == "")
                return "";
            if (strSex.Trim() == "0")
                return "性别不限";
            else if (strSex.Trim() == "1")
                return "男";
            else if (strSex.Trim() == "2")
                return "女";
            else
                return "";
        }

        /// <summary>
        /// 过滤非法关键词
        /// </summary>
        /// <param name="content">待过滤的内容</param>
        public static string FilterBadWords(this string content)
        {
            content = FormatSql(content);
            string[] badWords = { "自杀手册", "凌辱美少女", "毛泽东毛爷爷", "大祚榮", "校花沉沦记", "五奶小青", "江湖淫娘", "红楼绮梦", "骆冰淫传", "夫妇乐园", "阿里布达年代记", "爱神之传奇", "不良少女日记", "沧澜曲", "创世之子猎艳之旅", "熟女之惑", "风骚侍女", "海盗的悠闲", "黑星女侠", "狡猾的风水相师", "俪影蝎心", "秦青的幸", "四海龙女", "我的性启蒙", "伴我淫", "屠龙别记", "淫术炼金士", "十景缎", "舌战法庭", "少妇白洁", "风尘劫", "妇的哀羞", "哥言语录", "年春衫薄", "王子淫传", "少年阿宾", "禁断少女", "枪淫少妇", "淫间道", "嫩穴", "电车之狼", "淫水", "肉棍", "鸡吧", "鸡巴", "朱蒙", "出售走私", "浩方平台抽奖", "针孔摄像头", "出售银行", "出售发票", "迷昏", "失意药", "遗忘药", "失身药", "乙醚", "迷魂药", "迷魂", "失忆药", "手qiang", "迷幻", "mihuan", "mi幻", "qiang支", "枪zhi", "出售手枪", "卫星接收器", "香港GHB水", "透视", "老虎机", "轮盘机", "百家乐", "连线机", "模拟机", "彩票机", "礼品机", "火药制作", "麻醉枪", "监听", "监视", "海乐神", "酣乐欣", "三唑仑", "窃听", "三挫仑", "短信猫", "车牌反光", "江绵恒", "海归美女国内手眼通天", "激流中国", "富人与农民工", "98印尼", "华人惨案", "华国锋", "批评刘少奇", "李书凯", "蚁力神", "五毛党", "网络评论员工作指南", "邓小平", "高干子女名单", "鄧小平", "中国震惊世界", "江泽民", "吴官正无官", "罗干不干", "曾庆红不红", "黄菊早黄", "六合采", "六合彩", "六和彩", "白小姐", "踩江民谣", "罢食", "罢吃", "罢饭", "法輪", "镇压学生", "趙紫陽", "赵紫阳", "自由亚州", "人民报", "法lun功", "香港马会", "曾道人", "特码", "一码中特", "自由门", "李洪志", "大纪元", "真善忍", "新生网", "新唐人", "明慧", "无界浏览", "美国之音", "大紀元", "汕尾事件", "反中游行", "学生暴动", "死刑过程", "色空寺", "裸聊", "灭绝罪", "生成身份证", "身份证生成", "性用品", "性药品", "无界浏览器", "偷拍走光", "色情电影", "黄色电影", "黄色小电影", "rentiandiling", "xintangrendianshi", "全球春节晚会预告", "风骚少妇", "极品写真", "陈冠希裸照", "阿娇裸照", "裸体", "露点图片", "露点视频", "露点电影", "露点小电影", "漏点图片", "漏点电影", "漏点视频", "漏点小电影", "苹果被删", "胡锦涛和陈水扁对话", "两岸才子对话", "中国十大最恶心语录", "北京奥运会开幕式方案", "法轮", "法一轮", "阴唇", "阴户", "淫靡", "阴茎", "阴蒂", "乱伦", "手淫", "做鸡", "六四事件", "淫色", "K粉", "白粉", "大麻", "摇头丸", "出售猎枪", "手机定位", "钢珠手枪", "军用制式", "脱衣女郎", "脱衣美女", "脱衣MM", "赌球", "彩票预测", "球预测", "亚洲盘", "即时赔率", "移动电话拦截", "电表控制器", "针孔照相机", "防爆电击枪", "防爆喷雾剂", "出售气枪", "出售套牌车", "电子秤解码器", "地磅专用解码器", "电表专用调表器", "开锁专用工具", "DIY原子弹", "波佳达", "蜜洞", "菊花洞", "性学教授", "太王四神记", "凸点装", "国际足坛五大", "扶不起的阿斗", "出墙红杏之千岛湖之旅", "徐和柴学友", "遭警察殴打", "新诗年鉴", "李沛瑶", "美国凯德", "肥东交警", "针对台湾", "梁保华", "苏东解体", "反右题材", "信号拦截器", "山西洪洞", "巨额骗储", "东北风情熟女之惑", "海盗的悠闲生活", "黑天使", "混蛋神风流史", "秦青的幸福生活", "逃亡艳旅", "我的性启蒙老师", "现代艳帝传奇", "星光伴我淫", "倚天屠龙别记", "风月大陆", "美少妇的哀羞", "阿兵哥言语录", "遥想当年春衫薄", "神雕外传之郭襄", "睡着的武神", "汽车爆炸案", "周容重", "胡錦濤", "汕頭頻傳擄童割器官", "法輪功", "六决不", "清华网管", "卢跃刚", "最淫官员", "中国人都在上的四大当", "痛批政法委", "山西黑砖窑", "黑窑奴役", "敢坐飞机吗", "韩国身份证", "台湾身份证", "广安第二人民医院", "天鹅之旅", "玉蒲团", "践踏中国女性", "南京大学法学院", "挥发型迷药", "喷雾型迷药", "金伯帆", "崔英杰", "松花江污染", "仁寿警方", "愈快乐愈堕落麻醉枪", "唐人电视台", "金鳞岂是池中物", "江山美人志", "民警当副院长", "股市民谣", "卫星遭黑客攻击", "萬人暴", "官逼民反", "動乱", "军火价格", "劉奇葆", "女友坊", "奴役童工", "性奴", "奴事件", "二奶大奖赛", "性爱日记", "计生风暴", "厦门大游行", "二奶大赛", "纪股票市场五卅惨案", "毛爷爷复活", "智能H", "仿真假钞", "赣江学院", "江西田园置业集团", "高莺莺", "西藏禁书", "股民造反", "股民率先造反", "黑匣子最后", "抵制家乐福", "大恐怖杀手", "您的心被曾经的谎言", "金麟岂是池中物", "代理发票", "神州共震迎奥运", "三分钟禁播片", "日韩情欲电影", "夫妻两性电影少女人体图", "少女刺激电影", "全裸人体艺术图", "日本情欲", "妹妹艳情", "小妹妹艳爽", "少女浴室", "美女三点", "免费桃色", "激爽情欲", "夜生活夜电影", "美女午夜", "欧美情欲", "极品艳情", "国民党被推翻了么", "全国包二奶大赛", "高干子弟名单", "金晶的嫌疑人分析", "星爷搞笑发挥到如此境", "陈shuibian", "温jiabao", "胡jintao", "唐飞", "中國發表的東海開發", "民政局局长居然还摸", "福娃是魔咒", "股民暴动", "警察被指强奸女当事人", "四月圣火被搞怪", "必先撞其火车", "抢其火炬", "震其国土吓其国民", "中国是全球唯一绝对不", "个特权家庭垄断中国", "清海无上师", "代理国税", "代理海关", "代理地税", "龙的腾飞一定会有", "瓮安副县长", "出售工字牌气枪", "机打真发票", "刻章办证", "震后最流行幽默段子", "当今六大谎言", "东海协议", "当代无耻语录排行榜", "零八奥运会公式", "开平女生", "地下先烈们打电话", "阿扁推翻", "毛主席", "胡主席", "忠告股民", "法轮功", "忠告中国股民", "代开", "代办", "财税代理", "各类发票", "税务代理", "窃聽器", "楼主是猪" };
            for (int i = 0; i < badWords.Length; i++)
            {
                content = content.Replace(badWords[i], "");
            }
            return content;
        }

        
        #endregion

        #region ===mac===
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]

        private static extern Int32 inet_addr(string ip);

        /// <summary>
        /// 获取客户端的mac地址，取不到时取ip
        /// </summary>
        /// <returns></returns>
        public static string GetMac(string userip, string strClientIp)
        {
            var mac = "";
            try
            {
                var ldest = inet_addr(strClientIp); //目的地的ip 
                var macinfo = new Int64();
                var len = 6;
                var res = SendARP(ldest, 0, ref macinfo, ref len);
                var macSrc = macinfo.ToString("X");
                if (macSrc == "0")
                {
                    mac = GetIp();
                }

                while (macSrc.Length < 12)
                {
                    macSrc = macSrc.Insert(0, "0");
                }

                var macDest = "";

                for (var i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            macDest = macDest.Insert(0, macSrc.Substring(i, 2));
                        }
                        else
                        {
                            macDest = "-" + macDest.Insert(0, macSrc.Substring(i, 2));
                        }
                    }
                }
                mac = macDest;
            }
            catch
            {
                mac = GetIp();
            }
            if (mac.Length < 1)
                mac = GetIp();
            if (mac == "00-00-00-00-00-00")
            {

                mac = GetIp();

            }
            if (mac.Length > 30)
                mac = mac.Substring(0, 30);
            return mac;
        }

        #endregion

        #region ===unicode===
        /// <summary>
        /// 中文转unicode
        /// </summary>
        /// <returns></returns>
        public static string Chinese_To_Unicode(this string str)
        {
            var outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (var i = 0; i < str.Length; i++)
                {
                    outStr += "/u" + ((int)str[i]).ToString("x");
                }
            }
            return outStr;
        }
        /// <summary>
        /// unicode转中文
        /// </summary>
        /// <returns></returns>
        public static string Unicode_To_Chinese(this string str)
        {
            var outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                var strlist = str.Replace("/", "").Split('u');
                try
                {
                    for (var i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符  
                        outStr += (char)int.Parse(strlist[i], NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }
            return outStr;
        }


        /// <summary>
        /// unicode转中文（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Unicode_To_Chinese_Js(this string str)
        {
            var outStr = "";
            var reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate(Match m1)
            {
                return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
            });
            return outStr;
        }
        /// <summary>
        /// 中文转unicode（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Chinese_To_Unicode_Js(this string str)
        {
            var outStr = "";
            var a = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (var i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i].ToString(), @"[\u4e00-\u9fa5]")) { outStr += "\\u" + ((int)str[i]).ToString("x"); }
                    else { outStr += str[i]; }
                }
            }
            return outStr;
        } 
        #endregion

        #region === other ===

        /// <summary>
        /// 可枚举类型转化为字符串，类似于js中数组的join函数
        /// </summary>
        public static string Join<T>(IEnumerable<T> ss, string tag = "")
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(tag))
            {
                foreach (var s in ss)
                {
                    sb.Append(s);
                }
            }
            else
            {
                foreach (var s in ss)
                {
                    sb.Append(tag).Append(s);
                }
                if (sb.Length >= tag.Length)
                {
                    sb.Remove(0, tag.Length);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region === other 2 ===

        public static string StringToHex(this string source)
        {
            var length = source.Length;
            var sb = new StringBuilder();
            byte[] bytes;
            for (var i = 0; i < length; i++)
            {
                bytes = Encoding.Default.GetBytes(source.Substring(i, 1));
                var num5 = bytes.Length;
                if (num5 == 1)
                {
                    sb.Append(Convert.ToString(bytes[0], 0x10));
                }
                else
                {
                    sb.Append(Convert.ToString(bytes[0], 0x10)).Append(Convert.ToString(bytes[1], 0x10));
                }
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 把源字符串中含有非法词库中的非法词替换掉,默认替换成*号，可指定替换的字符
        /// 非法词库在 InvalidStringArray类中
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="replaceStr">要替换的字符串</param>
        /// <returns>返回结果</returns>
        public static string ReplaceInvalid(this string source, string replaceStr = "*")
        {
            return InvalidStringArray.ArrInvalidString1.Aggregate(source, (current, item) => current.Replace(item, replaceStr));
        }

        public static string HexToString(this string source)
        {
            var bytes = new byte[source.Length / 2];
            for (var i = 0; i < source.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(source.Substring(i, 2), 0x10);
            }
            return Encoding.Default.GetString(bytes);
        }
        public static string Replace(this string s, string str, string tag, int start, int end)
        {
            var r = s.Substring(start, end - start).Replace(str, tag);
            return s.Substring(0, start) + r + s.Substring(end, s.Length - end);
        }
        public static string ReplaceRegex(this string s, string regex, string tag)
        {
            return new Regex(regex).Replace(s, tag);
        }

        public static string Last(this string s, int n)
        {
            return s.Length > n ? s.Substring(s.Length - n) : s;
        }

        public static string[] Split(this string s, string tag, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            return s.Split(new[] { tag }, splitOptions);
        }
        public static string[] Split(this string s, string[] tags, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            return s.Split(tags, splitOptions);
        }

        public static string ReverseString(this string s)
        {
            return s.Reverse().Join();
        }

        public static string OrderString(this string s)
        {
            return s.OrderBy(a => a).Join();
        }

        public static string Formats(this string s, params object[] args)
        {
            if (args.Length == 1)
            {
                var arg = args[0] ?? "";
                var type = arg.GetType();
                if (type.Module.Name != "mscorlib.dll")
                {
                    return s.Formats(args[0].ToDictionary());
                }
            }
            return string.Format(s, args);
        }

        public static string Formats(this string s, Dictionary<string, object> dict)
        {
            return dict.Aggregate(s, (current, kv) => current.Replace("{" + kv.Key + "}", kv.Value == null ? "" : kv.Value.ToString()));
        }

        public static string Formats(this string s, KeyValuePair<string, string> pair)
        {
            return s.Replace("{" + pair.Key + "}", pair.Value);
        }

        public static string Formats(this string s, Dictionary<string, string> ps)
        {
            return ps.Aggregate(s, (current, v) => current.Formats(v));
        }

        public static string HtmlToText(this string html)
        {
            return Regex.Replace(html.Replace("\t", "").Replace("&nbsp;", "").ToDBC().Replace(" ", ""), "<[^<>]*>", "");
        }

        public static string HtmlDecode(this string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        public static string HtmlEncode(this string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        public static string UrlDecode(this string input, Encoding e)
        {
            return HttpUtility.UrlDecode(input, e);
        }

        public static string UrlDecode(this string input)
        {
            return input.UrlDecode(Encoding.UTF8);
        }

        public static string UrlEncode(this string input, Encoding e)
        {
            return HttpUtility.UrlEncode(input, e);
        }

        public static string UrlEncode(this string input)
        {
            return input.UrlEncode(Encoding.UTF8);
        }
        /// <summary>
        /// UrlEncodeToUpper,转码所得字符全部大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncodeToUpper(this string str)
        {
            var builder = new StringBuilder();
            foreach (var c in str)
            {
                var urlEncodeStr = HttpUtility.UrlEncode(c.ToString());
                if (urlEncodeStr != null && urlEncodeStr.Length > 1)
                {
                    builder.Append(urlEncodeStr.ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// UrlEncodeToUpper,转码所得字符全部大写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="e">编码格式</param>
        /// <returns></returns>
        public static string UrlEncodeToUpper(this string str, Encoding e)
        {
            var builder = new StringBuilder();
            foreach (var c in str)
            {
                var urlEncodeStr = HttpUtility.UrlEncode(c.ToString(), e);
                if (urlEncodeStr != null && urlEncodeStr.Length > 1)
                {
                    builder.Append(urlEncodeStr.ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        public static string XmlDecode(this string input)
        {
            return input.IsNullOrEmpty() ? string.Empty : input.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&apos;", "'").Replace("&quot;", "\"");
        }

        public static string XmlEncode(this string input)
        {
            return string.IsNullOrEmpty(input) ? string.Empty : input.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }

        /// <summary>
        /// 字符串是否为null或空
        /// </summary>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串是否非null或空
        /// </summary>
        public static bool NotNullOrEmpty(this string s)
        {
            return !s.IsNullOrEmpty();
        }

        /// <summary>
        /// 字符串是否非null或空格
        /// </summary>
        public static bool HasValue(this string s)
        {
            return s.NotNullOrEmpty() && s.Trim().NotNullOrEmpty();
        }

        /// <summary>
        /// 用逗号分割
        /// </summary>
        public static string[] SplitWithComma(this string input)
        {
            return input.IsNullOrEmpty()
                       ? new string[0]
                       : input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 用字符分割
        /// </summary>
        public static string[] SplitWithSeparator(this string input, char separator)
        {
            return input.IsNullOrEmpty() ? new string[0] : input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 用字符串分割
        /// </summary>
        public static string[] SplitWithString(this string input, string separator)
        {
            return input.IsNullOrEmpty() ? new string[0] : input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 将字符串转化为小写字母开头
        /// </summary>
        public static string BeginAsLower(this string s)
        {
            if (s.IsNullOrEmpty() || s.Trim().IsNullOrEmpty())
            {
                return "";
            }
            s = FormatId(s);

            s = s.Substring(0, 1).ToLower() + s.Substring(1);
            return s;
        }

        private static string FormatId(string s)
        {
            if (s.StartsWith("ID"))
            {
                s = "Id" + s.Substring(2);
            }
            if (s.EndsWith("ID"))
            {
                s = s.Substring(0, s.Length - 2) + "Id";
            }
            return s;
        }

        /// <summary>
        /// 将字符串转化为大写字母开头
        /// </summary>
        public static string BeginAsUpper(this string s)
        {
            if (s.IsNullOrEmpty() || s.Trim().IsNullOrEmpty())
            {
                return "";
            }
            s = FormatId(s);

            s = s.Substring(0, 1).ToUpper() + s.Substring(1);
            return s;
        }

        /// <summary>
        /// //计算字符串的长度（一个双字节字符长度计2，ASCII字符计1）
        /// </summary>
        /// <param name="s">指定字符串</param>
        /// <returns>字符串的长度</returns>
        public static int GetLength(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return 0;
            }
            return Regex.Replace(s, "[^\\x00-\\xff]", "aa", RegexOptions.None).Length;
        }

        /// <summary>
        /// 获取密码字符串等级 (弱、中、强,三个等级)
        /// </summary>
        /// <param name="s">指定字符串</param>
        /// <returns>等级(中文)</returns>
        public static string GetPassLevel(this string s)
        {
            var passLevel = 0;
            if (s.IsNullOrEmpty())
            {
                return string.Empty;
            }
            if (Regex.IsMatch(s, @"[0-9]"))
            {
                passLevel += 1;
            }
            if (Regex.IsMatch(s, @"[a-z]"))
            {
                passLevel += 1;
            }
            if (Regex.IsMatch(s, @"[A-Z]"))
            {
                passLevel += 1;
            }
            if (Regex.IsMatch(s, @"[^0-9A-Za-z]"))
            {
                passLevel += 2;
            }
            if (s.Length >= 6)
                passLevel += 1;
            if (s.Length >= 10)
                passLevel += 1;
            if (s.Length >= 12)
                passLevel += 1;
            if (s.Length >= 15)
                passLevel += 1;
            return passLevel < 4 ? "弱" : passLevel < 7 ? "中" : "强";
        }

        /// <summary>
        /// 字符串s包含字符c的个数
        /// </summary>
        public static int StringAt(this string s, char c)
        {
            if (s.IsNullOrEmpty() || s.IndexOf(c) == -1)
            {
                return 0;
            }

            return s.Split(c).Length - 1;
        }

        /// <summary>
        /// 字符串s包含字符c的个数
        /// </summary>
        public static int StringAt(this string s, string c)
        {
            if (s.IsNullOrEmpty() || s.IndexOf(c, StringComparison.Ordinal) == -1)
            {
                return 0;
            }

            return s.Split(c).Length - 1;
        }

        /// <summary>
        /// 截取姓名：中文算2个字符
        /// </summary>
        public static string NameCut(this string s, int l)
        {
            var result = s.StrCut(l);
            return result + (result.GetLength() == s.GetLength() ? "" : "***");
        }

        /// <summary>
        /// 截取字符串：中文算2个字符
        /// </summary>
        public static string StrCut(this string str, int length)
        {
            if (str.GetLength() < length)
            {
                return str;
            }

            var len = 0;
            byte[] b;
            var sb = new StringBuilder();

            for (var i = 0; i < str.Length; i++)
            {
                b = Encoding.Default.GetBytes(str.Substring(i, 1));
                if (b.Length > 1)
                    len += 2;
                else
                    len++;

                if (len > length)
                    break;

                sb.Append(str[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        ///按字节数截取姓名：中文算2个字节
        ///结果都加上***
        /// </summary>
        public static string CutName(this string str, int l)
        {
            var b = Encoding.GetEncoding("gb2312").GetBytes(str);
            if (b.Length <= l)
            {
                return str + "***";
            }
            return Encoding.GetEncoding("gb2312").GetString(b, 0, l).TrimEnd('?') + "***";
        }

        /// <summary>
        ///名字中数字个数大于l个
        ///则截取l个字符后加上***
        /// </summary>
        public static string FilterName(this string str, int l)
        {
            return str.NotNullOrEmpty() ? str.Length > 2 ? str.Substring(0, 2) + "***" : str + "***" : "";
        }

        /// <summary>
        /// 清除最前最后指定字符
        /// </summary>
        public static string ClearStratEndChar(this string str, char c)
        {
            var q = str.StartsWith("" + c + "") ? str.Substring(1) : str;//去掉最前
            var h = q.EndsWith("" + c + "") ? q.Substring(0, q.Length - 1) : q;//去掉最后
            return h.Trim();
        }

        /// <summary>
        /// 验证是否敏感字符
        /// </summary>
        /// <param name="regName"></param>
        /// <returns></returns>
        public static bool CheckRegName(this string regName)
        {
            return InvalidStringArray.ArrInvalidString1.Count(regName.Contains) > 0;
        }


        /// <summary>
        /// 冒泡排序法
        /// </summary>
        public static string[] BubbleSort(this string[] r)
        {
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (String.CompareOrdinal(r[j + 1], r[j]) < 0) //交换条件
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }

            return r;
        }

        /// <summary>
        /// Cson:Csharp Object Notation 支持pulic类型
        /// </summary>
        public static List<T> CsonToList<T>(this string s, List<T> defaultValue = null)
        {
            if (s.IsNullOrEmpty())
            {
                return defaultValue;
            }
            var result = new List<T>();
            try
            {
                var datas = s.Split('η').Select(a => a.Split('δ')).ToArray();
                var columns = datas[0];
                datas = datas.Skip(1).ToArray();
                var type = typeof(T);
                var constructor = type.GetConstructor(new Type[0]);
                if (constructor == null)
                {
                    return defaultValue;
                }

                var fields = type.GetFields();
                if (fields.Length > 0)
                {
                    var fis = new Dictionary<string, FieldInfo>();
                    foreach (var field in fields)
                    {
                        fis[field.Name] = field;
                    }
                    var count = columns.Length;
                    FieldInfo f;
                    T obj;
                    object v;
                    foreach (var data in datas)
                    {
                        obj = (T)constructor.Invoke();
                        for (var i = 0; i < count; i++)
                        {
                            if (fis.TryGetValue(columns[i], out f))
                            {
                                v = data[i];
                                try
                                {
                                    v = Convert.ChangeType(v, f.FieldType);
                                }
                                catch
                                {
                                    v = f.FieldType.DefaultValue();
                                }
                                f.SetValue(obj, v);
                            }
                        }
                        result.Add(obj);
                    }
                }
                else
                {
                    var propertys = type.GetProperties();
                    var pis = new Dictionary<string, PropertyInfo>();
                    foreach (var property in propertys)
                    {
                        pis[property.Name] = property;
                    }
                    var count = columns.Length;
                    PropertyInfo p;
                    T obj;
                    object v;
                    foreach (var data in datas)
                    {
                        obj = (T)constructor.Invoke();
                        for (var i = 0; i < count; i++)
                        {
                            if (pis.TryGetValue(columns[i], out p))
                            {
                                v = data[i];
                                try
                                {
                                    v = Convert.ChangeType(v, p.PropertyType);
                                }
                                catch
                                {
                                    v = p.PropertyType.DefaultValue();
                                }
                                p.SetValue(obj, v, null);
                            }
                        }
                        result.Add(obj);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return defaultValue;
            }
            return result;
        }


        /// <summary>
        /// Sql过滤
        /// </summary>
        public static string SqlFilter(this string sql)
        {
            if (sql.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return new[] { "insert", "delete ", "select", "update ", "exec ", "varchar", "drop", "create ", "declare", "truncate", "cursor", "begin ", "open ", "<--", "-->", "--", "'", ";", "\n" }.Any(sql.ToLower().Contains) ? string.Empty : sql;
        }

        /// <summary>
        /// 防XSS注入
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string XssFilter(this string s)
        {
            if (s.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return HttpUtility.HtmlEncode(s);
        }

        public static string Match(this string s, string pattern, string defaultValue = "")
        {
            return s.Match<string>(pattern, RegexOptions.None, defaultValue);
        }
        public static string Match(this string s, string pattern, RegexOptions options, string defaultValue = "")
        {
            return s.Match<string>(pattern, options, defaultValue);
        }
        public static T Match<T>(this string s, string pattern, RegexOptions options = RegexOptions.None, T defaultValue = default(T))
        {
            try
            {
                return Regex.Match(s, pattern, options).Value.ConvertTo(defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static bool IsMatch(this string s, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.IsMatch(s, pattern, options);
        }
        public static string[] Matches(this string s, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.Matches(s, pattern, options).Cast<Match>().Select(a => a.Value).ToArray();
        }

        public static string ToHtmlCode(this string sourceStr)
        {
            return sourceStr.Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "''").Replace(" ", "&nbsp;").Replace("\r\n", "<br/>").Replace("\n", "<br/>").Trim();
        }
        public static string ToTextCode(this string sourceStr)
        {
            return sourceStr.Replace("&lt;", "<").Replace("&gt;", ">").Replace("''", "'").Replace("&nbsp;", " ").Replace("<br/>", "\r\n").Replace("<br>", "\n").Trim();
        }
        public static string HtmlTextCut(this string input, int length)
        {
            if (length < 0)
            {
                length = 0;
            }
            length *= 2;
            if (!input.Contains("<body>"))
            {
                input = "<body>" + input;
            }
            input = input.HtmlToText();
            return StrCut(input, length);
        }

        /// <summary>
        /// 截取 start 和 end 之间的字符串
        /// </summary>
        public static string Substring(this string str, string start, string end)
        {
            if (str == null)
            {
                return "";
            }
            var startIndex = str.IndexOf(start, StringComparison.Ordinal);
            if (startIndex == -1)
            {
                return "";
            }
            startIndex += start.Length;
            var eneIndex = str.IndexOf(end, startIndex, StringComparison.Ordinal);
            if (eneIndex == -1)
            {
                return "";
            }
            return str.Substring(startIndex, eneIndex - startIndex);
        }

        /// <summary>
        /// 截取 start 之后的字符串
        /// </summary>
        public static string Substring(this string str, string start)
        {
            if (str == null)
            {
                return "";
            }
            var startIndex = str.IndexOf(start, StringComparison.Ordinal);
            if (startIndex == -1)
            {
                return "";
            }
            startIndex += start.Length;
            return str.Substring(startIndex);
        }

        /// <summary>
        /// 大于
        /// </summary>
        public static bool BigerThan(this string source, string target, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Compare(source, target, stringComparison) > 0;
        }
        /// <summary>
        /// 小于
        /// </summary>
        public static bool SmallerThan(this string source, string target, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Compare(source, target, stringComparison) < 0;
        }
        /// <summary>
        /// 大于等于
        /// </summary>
        public static bool BigerEqual(this string source, string target, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Compare(source, target, stringComparison) >= 0;
        }
        /// <summary>
        /// 小于等于
        /// </summary>
        public static bool SamllerEqual(this string source, string target, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return string.Compare(source, target, stringComparison) <= 0;
        }

        public static bool StartWith(this string str, IEnumerable<string> es)
        {
            return es.Any(str.StartsWith);
        }
        public static bool StartWith(this string str, params string[] ps)
        {
            return ps.Any(str.StartsWith);
        }

        public static List<int> IndexOfs(this string str, string s, StringComparison comparisonType = StringComparison.Ordinal)
        {
            var startPos = 0;
            var foundPos = 0;
            var result = new List<int>();

            while (foundPos > -1 && startPos < str.Length)
            {
                foundPos = str.IndexOf(s, startPos, comparisonType);
                if (foundPos > -1)
                {
                    startPos = foundPos + 1;
                    result.Add(foundPos);
                }
            }

            return result;
        }

        /// <summary>
        /// 正则方式获取含有字符串索引
        /// </summary>
        public static int RegexIndexOf(this string str, string regex, RegexOptions regexOption = RegexOptions.None)
        {
            var ms = Regex.Match(str, regex, regexOption);
            return ms.Index;
        }

        /// <summary>
        /// 正则方式获取含有字符串索引
        /// </summary>
        public static List<int> RegexIndexOfs(this string str, string regex, RegexOptions regexOption = RegexOptions.None)
        {
            var result = new List<int>();
            var ms = Regex.Matches(str, regex, regexOption);
            if (ms.Count > 0)
            {
                result.AddRange(ms.OfType<Match>().Select(a => a.Index));
            }
            return result;
        }

        /// <summary>
        /// 替换字符串起始位置(开头)中指定的字符串
        /// </summary>
        /// <param name="s">源串</param>
        /// <param name="searchStr">查找的串</param>
        /// <param name="replaceStr">替换的目标串</param>
        /// <returns></returns>
        public static string TrimStarString(this string s, string searchStr, string replaceStr)
        {
            var result = s;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    return result;
                }
                if (s.Length < searchStr.Length)
                {
                    return result;
                }
                if (s.IndexOf(searchStr, 0, searchStr.Length, StringComparison.Ordinal) > -1)
                {
                    result = s.Substring(searchStr.Length);
                }
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return result;
            }
        }
        /// <summary>
        /// 替换字符串末尾位置中指定的字符串
        /// </summary>
        /// <param name="s">源串</param>
        /// <param name="searchStr">查找的串</param>
        /// <param name="replaceStr">替换的目标串</param>
        public static string TrimEndString(this string s, string searchStr, string replaceStr)
        {
            var result = s;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    return result;
                }
                if (s.Length < searchStr.Length)
                {
                    return result;
                }
                if (s.IndexOf(searchStr, s.Length - searchStr.Length, searchStr.Length, StringComparison.Ordinal) > -1)
                {
                    result = s.Substring(0, s.Length - searchStr.Length);
                }
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return result;
            }
        }
        #endregion
    }
}
