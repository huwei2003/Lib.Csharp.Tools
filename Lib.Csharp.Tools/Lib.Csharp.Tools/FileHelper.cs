﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lib.Csharp.Tools
{
    public class FileHelper
    {
        #region 静态底层操作类
        public static string Reader(string path)
        {
            return Reader(HttpContext.Current.Server.MapPath(path), "gb2312");
        }

        public static void Delete(string path)
        {
            EDelete(HttpContext.Current.Server.MapPath(path));
        }

        public static void Writer(string path, string text)
        {
            Writer(HttpContext.Current.Server.MapPath(path), text, "gb2312");
        }

        public static string Replace(string source, Hashtable ht)
        {
            foreach (DictionaryEntry de in ht)
            {
                source = source.Replace(de.Key.ToString(), de.Value.ToString());
            }
            return source;
        }
        /// <summary>
        /// 生成静态方法
        /// </summary>
        /// <param name="tempPath">模板文件路径</param>
        /// <param name="htmlPath">要生成的静态页面路径</param>
        /// <param name="ht">替换的内容组</param>
        /// <param name="readCoding">读文件的编码</param>
        /// <param name="writecoding">写文件的编码</param>
        public static void ToStaic(string tempPath, string htmlPath, Hashtable ht, string readCoding, string writecoding)
        {
            string tempContent = Reader(tempPath, readCoding);
            string htmlContent = Replace(tempContent, ht);
            Writer(htmlPath, htmlContent, writecoding);

        }
        private static StreamReader _sr;
        private static StreamWriter _sw;

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="coding">文件编码</param>
        /// <returns></returns>
        public static string Reader(string path, string coding)
        {
            string str = "";
            if (File.Exists(path))
            {
                try
                {
                    _sr = new StreamReader(path, Encoding.GetEncoding(coding));
                    str = _sr.ReadToEnd();
                    _sr.Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }

            return str;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="text">写入内容</param>
        /// <param name="coding">文件编码</param>
        public static void Writer(string path, string text, string coding)
        {
            if (File.Exists(path))
            {
                EDelete(path);
            }
            try
            {
                if (coding.ToUpper() == "UTF-8")
                {
                    _sw = new StreamWriter(path, false, Encoding.UTF8);
                }
                else
                {
                    _sw = new StreamWriter(path, false, Encoding.GetEncoding(coding));
                }
                _sw.WriteLine(text);
                _sw.Flush();
                _sw.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        private static void EDelete(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        /// <summary>
        /// 根据指定路径，获取其下所有文件列表，
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns>文件名 list</returns>
        public static List<string> GetFileInfo(string filePath)
        {
            FileSystemInfo fileinfo = new DirectoryInfo(filePath);
            var filelist = ListFileSort(fileinfo);
            return filelist;
        }

        /// <summary>
        /// 获取其下所有文件列表
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        public static List<string> ListFileSort(FileSystemInfo fileinfo)
        {
            var filelist = new List<string>();
            var indent = 0;
            if (!fileinfo.Exists) return null;
            var dirinfo = fileinfo as DirectoryInfo;
            if (dirinfo == null) return null; //不是目录
            indent++;//缩进加一
            var files = dirinfo.GetFileSystemInfos();
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i] as FileInfo;
                if (file != null) // 是文件
                {
                    filelist.Add(file.Name);

                }
                else   //是目录
                {
                    //this.richTextBox1.Text += files[i].FullName + "/r/n/r/n";
                    //sb.Append(files[i].FullName + "/r/n/r/n");
                    ListFileSort(files[i]);  //对子目录进行递归调用
                }
            }
            indent--;//缩进减一
            return filelist;
        }

        #endregion
    }
}
