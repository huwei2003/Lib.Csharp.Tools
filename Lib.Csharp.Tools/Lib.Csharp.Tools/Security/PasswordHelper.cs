using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lib.Csharp.Tools.Security
{
    /// <summary>
    /// 加解密帮组类
    /// </summary>
    public class PasswordHelper
    {
        #region 字符串 Des 加解密

        /// <summary>
        /// 字符串加密 Des
        /// </summary>
        /// <param name="strText">要加密的字符串</param>
        /// <param name="strEncrKey">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string DesEncrypt(string strText, string strEncrKey)
        {

            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                var byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, strEncrKey.Length));
                var des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception error)
            {
                return "error:" + error.Message + "\r";
            }
        }

        /// <summary>
        /// 字符串解密 Des
        /// </summary>
        /// <param name="strText">要解密的字符串</param>
        /// <param name="sDecrKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DesDecrypt(string strText, string sDecrKey)
        {
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {
                var byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Convert.FromBase64String(strText);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception error)
            {
                return "error:" + error.Message + "\r";
            }

        }

        #endregion


        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">待加密串</param>
        /// <param name="code">指定为16或32位</param>
        /// <returns></returns>
        public static string Md5(string str, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密 
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }

        #endregion
    }
}
