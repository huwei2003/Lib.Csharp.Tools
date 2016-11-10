using System;
using System.IO;
using log4net;
using log4net.Config;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// Log4Net 日志操作类
    /// </summary>
    public class Log4NetHelper
    {
        private static readonly ILog Log = LogManager.GetLogger("Admin");

         static Log4NetHelper()
        {
            var logConfig = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/log4net.config";
            XmlConfigurator.ConfigureAndWatch(
                new FileInfo(logConfig));
        }

        public static void Debug(string message)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug(message);
            }
        }
        /// <summary>
        /// 记录异常的相关信息,log4net
        /// </summary>
        /// <param name="ex1"></param>
        public static void Debug(Exception ex1)
        {
            if (Log.IsDebugEnabled)
            {
                if (ex1 != null)
                {
                    Log.Debug(ex1.Message.ToString() + "\r\n" + ex1.Source.ToString() + "\r\n" + ex1.TargetSite.ToString() + "\r\n" + ex1.StackTrace.ToString());
                }
            }

        }
        public static void Error(string message)
        {
            if (Log.IsErrorEnabled)
            {
                Log.Error(message);
            }
        }
        public static void Fatal(string message)
        {

            if (Log.IsFatalEnabled)
            {
                Log.Fatal(message);
            }
        }
        public static void Info(string message)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info(message);
            }
        }

        public static void Warn(string message)
        {
            if (Log.IsWarnEnabled)
            {
                Log.Warn(message);
            }
        }
    }
}
