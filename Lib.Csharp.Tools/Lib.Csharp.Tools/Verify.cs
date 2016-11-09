using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 验证类
    /// </summary>
    public static class Verify
    {
        #region 验证文本框输入为数字
        /// <summary>
        /// 验证是不是数字（包含整数和小数）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNum(this string str)
        {
            return Regex.IsMatch(str, @"^[-]?\d+[.]?\d*$");
        }
        #endregion

        #region 验证文本框输入为整数
        /// <summary>
        /// 验证文本框输入为整数
        /// </summary>
        /// <param name="strNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsInteger(this string strNum)
        {
            return Regex.IsMatch(strNum, "^[0-9]*$");
        }
        #endregion

        #region 验证文本框输入为日期
        /// <summary>
        /// 判断日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsValidDate(this string date)
        {
            //验证YYYY-MM-DD格式,基本上把闰年和2月等的情况都考虑进去
            bool bValid = Regex.IsMatch(date, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
            return (bValid && String.Compare(date, "1753-01-01", StringComparison.Ordinal) >= 0);

            //将平年和闰年的日期验证表达式合并，我们得到最终的验证日期格式为YYYY-MM-DD的正则表达式为：

            //(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|
            //[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-
            //(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|
            //(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|
            //[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)


        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(this string str)
        {
            return Regex.IsMatch(str, @"（\d{4}）-（\d{1,2}）-（\d{1,2}）");
        }

        #endregion

        #region 验证文本框输入为电子邮件
        //验证电子邮件
        public static bool IsValidEmail(this string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region  验证文本框输入为电话号码
        /// <summary>
        /// 验证文本框输入为电话号码
        /// </summary>
        /// <param name="strPhone">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsPhone(this string strPhone)
        {
            return Regex.IsMatch(strPhone, @"\d{3,4}-\d{7,8}");
        }
        #endregion

        #region  验证文本框输入为传真号码
        /// <summary>
        /// 验证文本框输入为传真号码
        /// </summary>
        /// <param name="strFax">输入字符串</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool IsFax(this string strFax)
        {
            return Regex.IsMatch(strFax, @"86-\d{2,3}-\d{7,8}");
        }
        #endregion



        #region 验证文本是否由字母数字组成的普通字符串
        //验证电子邮件
        public static bool IsNormalString(this string strIn)
        {
            return Regex.IsMatch(strIn, @"^[0-9a-zA-Z]*$");
        }
        #endregion

        public static bool IsChineseLetter(this string value)
        {
            return Regex.IsMatch(value, @"^[\u4e00-\u9fa5]*$");
        }

        public static bool IsContactPhone(this string value)
        {
            return Regex.IsMatch(value, @"^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,11}$");
        }

        public static bool IsContainSpecialChar(this string source)
        {
            return Regex.IsMatch(source, @"[/\<>:.?*|$]");
        }

        public static bool IsDateDay(this string value)
        {
            return Regex.IsMatch(value, @"^((19)|(20))\d{2}-((0[1-9])|(1[012]))-((0[1-9])|([12]\d)|(3[01]))$");
        }

        public static bool IsDateMonth(this string value)
        {
            return Regex.IsMatch(value, "^(0?[[1-9]|1[0-2])$");
        }

        public static bool IsDecimal(this string value, int precision)
        {
            return Regex.IsMatch(value, @"^[+-]?\d+.[0-9]{" + precision + "}$");
        }

        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsEnglish(this string value)
        {
            return Regex.IsMatch(value, "^[A-Za-z]+$");
        }

        public static bool IsInt(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        public static bool IsLessLength(this string value, int length)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return (value.Length <= length);
        }

        public static bool IsMobile(this string value)
        {
            return !String.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^1[34578]\d{9}$");
        }

        public static bool IsNumeral(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d+[.]?\d*$");
        }

        public static bool IsChinese(this string value)
        {
            return Regex.IsMatch(value, "^[\u4e00-\u9fa5]+$");
        }

        public static bool IsQq(this string value)
        {
            return Regex.IsMatch(value, @"^[1-9]\d{4,11}$");
        }

        public static bool IsTelephone(this string value)
        {
            return Regex.IsMatch(value, @"^(\d{3,4}-)?\d{6,8}$");
        }

        public static bool IsTime(this string source)
        {
            return Regex.IsMatch(source,
                @"^((20)|(19))\d{2}[-]((0[1-9])|(1[0-2]))[-]((0[1-9])|([1-2][0-9])|(3[0-1]))\s(([0-1][0-9])|(2[0-3])):(([0-4][0-9])|(5[0-9])):(([0-4][0-9])|(5[0-9]))$");
        }

        public static bool IsIp(this string text)
        {
            if ((text == null) ||
                !Regex.IsMatch(text.Trim(), @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
            {
                return false;
            }
            return true;
        }

        public static bool IsUrl(this string value)
        {
            return Regex.IsMatch(value,
                "^http:\\/\\/[A-Za-z0-9]+\\.[A-Za-z0-9]+[\\/=\\?%\\-&_~`@[\\]\\':+!]*([^<>\\\"])*$");
        }

        public static bool RegexValidate(this string strInput, string strPattern)
        {
            return Regex.IsMatch(strInput, strPattern);
        }

        /// <summary>
        /// 校验银行卡卡号(Luhm校验)
        /// </summary>
        /// Luhm校验规则：16位银行卡号（19位通用）:
        /// 1.将未带校验位的 15（或18）位卡号从右依次编号 1 到 15（18），位于奇数位号上的数字乘以 2。
        /// 2.将奇位乘积的个十位全部相加，再加上所有偶数位上的数字。
        /// 3.将加法和加上校验位能被 10 整除。
        public static bool IsBankCard(this string bankno)
        {
            //银行卡号长度必须在16到19之间  或者 全数字
            if (bankno.Length < 16 || bankno.Length > 19 || (!bankno.IsMatch("^\\d*$")))
            {
                return false;
            }
            //开头6位
            const string strBin =
                "10,18,30,35,37,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,58,60,62,65,68,69,84,87,88,94,95,98,99";
            if (strBin.IndexOf(bankno.Substring(0, 2), StringComparison.Ordinal) == -1)
            {
                return false;
            }
            var lastNum = bankno.Substring(bankno.Length - 1, 1).ToInt32(); //取出最后一位（与luhm进行比较）
            var first15Num = bankno.Substring(0, bankno.Length - 1); //前15或18位
            var newArr = new List<int>();
            for (var i = first15Num.Length - 1; i > -1; i--)
            {
                //前15或18位倒序存进数组
                newArr.Add(first15Num.Substring(i, 1).ToInt32());
            }
            var arrJiShu = new List<int>(); //奇数位*2的积 <9
            var arrJiShu2 = new List<int>(); //奇数位*2的积 >9

            var arrOuShu = new List<int>(); //偶数位数组
            for (var j = 0; j < newArr.Count; j++)
            {
                if ((j + 1) % 2 == 1)
                {
                    //奇数位
                    if (newArr[j] * 2 < 9)
                        arrJiShu.Add(newArr[j] * 2);
                    else
                        arrJiShu2.Add(newArr[j] * 2);
                }
                else //偶数位
                    arrOuShu.Add(newArr[j]);
            }

            var jishuChild1 = new List<int>(); //奇数位*2 >9 的分割之后的数组个位数
            var jishuChild2 = new List<int>(); //奇数位*2 >9 的分割之后的数组十位数
            for (var h = 0; h < arrJiShu2.Count; h++)
            {
                jishuChild1.Add(arrJiShu2[h] % 10);
                jishuChild2.Add(arrJiShu2[h] / 10);
            }

            var sumJiShu = arrJiShu.Sum(); //奇数位*2 < 9 的数组之和
            var sumOuShu = arrOuShu.Sum(); //偶数位数组之和
            var sumJiShuChild1 = 0; //奇数位*2 >9 的分割之后的数组个位数之和
            var sumJiShuChild2 = 0; //奇数位*2 >9 的分割之后的数组十位数之和
            for (var p = 0; p < jishuChild1.Count; p++)
            {
                sumJiShuChild1 = sumJiShuChild1 + jishuChild1[p];
                sumJiShuChild2 = sumJiShuChild2 + jishuChild2[p];
            }
            //计算总和
            var sumTotal = sumJiShu + sumOuShu + sumJiShuChild1 + sumJiShuChild2;

            //计算Luhm值
            var k = sumTotal % 10 == 0 ? 10 : sumTotal % 10;
            var luhm = 10 - k;

            return lastNum == luhm;
        }

        /// <summary>
        /// 校验身份证
        /// </summary>
        public static bool IsIdCard(this string id)
        {
            const string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            switch (id.Length)
            {
                case 18:
                {
                    long n;
                    if (Int64.TryParse(id.Remove(17), out n) == false || n < Math.Pow(10, 16) ||
                        Int64.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
                    {
                        return false; //数字验证
                    }

                    if (address.IndexOf(id.Remove(2)) == -1)
                    {
                        return false; //省份验证
                    }
                    var birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                    DateTime time;
                    if (DateTime.TryParse(birth, out time) == false)
                    {
                        return false; //生日验证
                    }
                    var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                    var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                    var ai = id.Remove(17).ToCharArray();
                    var sum = 0;
                    for (var i = 0; i < 17; i++)
                    {
                        sum += Int32.Parse(wi[i]) * Int32.Parse(ai[i].ToString());
                    }
                    int y;
                    Math.DivRem(sum, 11, out y);
                    if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
                    {
                        return false; //校验码验证
                    }
                    return true; //符合GB11643-1999标准
                }
                case 15:
                {
                    long n;
                    if (Int64.TryParse(id, out n) == false || n < Math.Pow(10, 14))
                    {
                        return false; //数字验证
                    }
                    if (address.IndexOf(id.Remove(2)) == -1)
                    {
                        return false; //省份验证
                    }
                    var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                    DateTime time;
                    if (DateTime.TryParse(birth, out time) == false)
                    {
                        return false; //生日验证
                    }
                    return true; //符合15位身份证标准
                }
                default:
                    return false;
            }
        }

        /// <summary>
        /// 验证组织机构代码,如：73972365-3
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsOrgCode(this string code)
        {
            if (code.IsNullOrEmpty() || code.Length != 10)
            {
                return false;
            }
            int[] wArray = { 3, 7, 9, 10, 5, 8, 4, 2 };
            code = code.ToUpper();
            char c;
            int zz = 0, z = 0;
            for (int i = 0; i <= 7; i++)
            {
                c = Char.Parse(code.Substring(i, 1));
                if (c >= 'A' && c <= 'Z')  //A-Z字符  
                {
                    z = (c - 55) * wArray[i];
                }
                else if (c >= '0' && c <= '9')  //0-9字符  
                {
                    z = Int32.Parse(c.ToString()) * wArray[i];
                }
                zz += z;
            }
            string c9;
            int jav = 11 - (zz % 11);
            if (jav == 10)
            {
                c9 = "X";
            }
            else if (jav == 11)
            {
                c9 = "0";
            }
            else
            {
                c9 = jav.ToString().Trim(); //删除文本前导空格  
            }
            return code == (code.Substring(0, 8) + "-" + c9);
        }

        /// <summary>
        /// 验证营业执照代码,如：110114011384873
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsBusCode(this string code)
        {
            bool result = false;
            if (code.IsNullOrEmpty() || code.Length != 15)
            {
                return false;
            }
            if (code.Length == 15)
            {
                var s = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                var p = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                var a = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                int m = 10;
                p[0] = m;
                for (var i = 0; i < code.Length; i++)
                {
                    a[i] = Convert.ToInt32(code.Substring(i, (i + 1) - i), m);
                    s[i] = (p[i] % (m + 1)) + a[i];
                    if (0 == s[i] % m)
                    {
                        p[i + 1] = 10 * 2;
                    }
                    else
                    {
                        p[i + 1] = (s[i] % m) * 2;
                    }
                }
                result = (1 == (s[14] % m));
            }
            return result;
        }

        /// <summary>
        /// 验证统一社会信用代码,如：91440300775592785U
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsBusNumber(this string code)
        {
            if (code.IsNullOrEmpty() || code.Length != 18)
            {
                return false;
            }
            const string baseCode = "0123456789ABCDEFGHJKLMNPQRTUWXY";
            var codes = new Dictionary<char, int>();
            for (int i = 0; i < baseCode.Length; i++)
            {
                codes.Add(baseCode[i], i);
            }
            var check = code[17]; if (!baseCode.Contains(check))
            {
                return false;
            }
            int[] wi = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };
            var sum = 0;
            for (int i = 0; i < 17; i++)
            {
                var key = code[i]; if (!baseCode.Contains(key))
                {
                    return false;
                }
                sum += (codes[key] * wi[i]);
            }
            var value = 31 - sum % 31;
            return value.Equals(codes[check]);
        }
    }
}
