﻿using System;
using log4net;

namespace Lib.Csharp.Tools
{
    public class Log
    {
        
        private static ILog log = LogManager.GetLogger("Admin");
        public static void Debug(string message)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        /// <summary>
        /// 记录异常的相关信息,log4net
        /// </summary>
        /// <param name="ex1"></param>
        public static void Debug(Exception ex1)
        {
            if (log.IsDebugEnabled)
            {
                if (ex1 != null)
                {
                    log.Debug(ex1.Message.ToString() + "\r\n" + ex1.Source.ToString() + "\r\n" + ex1.TargetSite.ToString() + "\r\n" + ex1.StackTrace.ToString());
                }
            }
        }

        public static void Error(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Error(Exception ex1)
        {
            if (log.IsErrorEnabled)
            {
                if (ex1 != null)
                {
                    log.Debug(ex1.Message.ToString() + "\r\n" + ex1.Source.ToString() + "\r\n" + ex1.TargetSite.ToString() + "\r\n" + ex1.StackTrace.ToString());
                }
            }
        }

        public static void Fatal(string message)
        {

            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Info(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public static void Warn(string message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        public static void Monitor(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        
    }
}
