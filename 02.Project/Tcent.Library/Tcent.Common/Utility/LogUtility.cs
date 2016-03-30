using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcent.Common
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    /// { Created At Time:[ 2016/3/16 18:48 ], By User:wcj21259, On Machine:WCJ }
    public class LogUtility
    {
        #region 初始化配置

        static LogUtility()
        {
            string defaultPath = AppDomain.CurrentDomain.BaseDirectory + "Configs\\log4net.config";
            XmlConfigurator.Configure(new FileInfo(defaultPath));
        }

        #endregion

        #region 02.枚举类型定义

        /// <summary>
        /// 消息类型
        /// </summary>
        public enum LogMessageType
        {
            /// <summary>
            /// 调试
            /// </summary>
            Debug,
            /// <summary>
            /// 信息
            /// </summary>
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            Warn,
            /// <summary>
            /// 错误
            /// </summary>
            Error,
            /// <summary>
            /// 致命错误
            /// </summary>
            Fatal
        }

        public enum LogType
        {
            /// <summary>
            /// 记录异常
            /// </summary>
            LogException,
            /// <summary>
            /// 公司内部接口异常
            /// </summary>
            ApiInnerError,
            /// <summary>
            /// 外部公司接口异常
            /// </summary>
            ApiOutError
        }

        public enum RequestType
        {
            /// <summary>
            /// 01.系统级别(Debug)
            /// </summary>
            Auto,
            /// <summary>
            /// 02.手工级别(INFO)
            /// </summary>
            Manual
        }

        #endregion

        #region 03.公开写日志的方法

        /// <summary>
        /// 记录Warn信息
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            DoLog(message, LogMessageType.Warn);
        }

        /// <summary>
        /// 记录Warn信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Warn(string message, Exception ex)
        {
            DoLog(message, LogMessageType.Warn, ex);
        }

        /// <summary>
        /// 记录Warn信息带模块信息
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="message">message</param>
        public static void Warn(string model, string message)
        {
            DoLog("{" + model + "}," + message, LogMessageType.Warn);
        }

        /// <summary>
        /// 记录Warn信息带模块信息
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="message">message</param>
        /// <param name="ex">ex</param>
        public static void Warn(string model, string message, Exception ex)
        {
            DoLog("{" + model + "}," + message, LogMessageType.Warn, ex);
        }

        /// <summary>
        /// 记录Error信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(string message)
        {
            DoLog(message);
        }

        /// <summary>
        /// 记录Error信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Error(string message, Exception ex)
        {
            DoLog(message, LogMessageType.Error, ex);
        }

        /// <summary>记录Error信息带模块信息</summary>
        /// <param name="model">model</param>
        /// <param name="message">message</param>
        public static void Error(string model, string message)
        {
            DoLog("{" + model + "}," + message);
        }

        // <summary>
        /// 记录Error信息带模块信息
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="message">message</param>
        /// <param name="ex">ex</param>
        public static void Error(string model, string message, Exception ex)
        {
            DoLog("{" + model + "}," + message, LogMessageType.Error, ex);
        }

        /// <summary>
        /// 记录Fatal信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Fatal(string message)
        {
            DoLog(message, LogMessageType.Fatal);
        }

        /// <summary>
        /// 记录Fatal信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal(string message, Exception ex)
        {
            DoLog(message, LogMessageType.Fatal, ex);
        }

        /// <summary>记录Fatal信息带模块信息</summary>
        /// <param name="model">model</param>
        /// <param name="message">消息</param>
        public static void Fatal(string model, string message)
        {
            DoLog("{" + model + "}," + message, LogMessageType.Fatal);
        }

        /// <summary>
        /// 记录Fatal信息带模块信息
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="message">message</param>
        /// <param name="ex">ex</param>
        public static void Fatal(string model, string message, Exception ex)
        {
            DoLog("{" + model + "}," + message, LogMessageType.Fatal, ex);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="exception">异常</param>
        /// <param name="logType">日志类型</param>
        private static void DoLog(string message, LogMessageType messageType = LogMessageType.Error,
            Exception exception = null, LogType logType = LogType.LogException)
        {
            ILog logger = LogManager.GetLogger(logType.ToString() + "_logger");

            switch (messageType)
            {
                case LogMessageType.Debug:
                    {
                        if (exception != null)
                        {
                            logger.Debug(message, exception);
                        }
                        else
                        {
                            logger.Debug(message);
                        }
                    }
                    break;

                case LogMessageType.Info:
                    {
                        if (exception != null)
                        {
                            logger.Info(message, exception);
                        }
                        else
                        {
                            logger.Info(message);
                        }
                    }
                    break;

                case LogMessageType.Warn:
                    {
                        if (exception != null)
                        {
                            logger.Warn(message, exception);
                        }
                        else
                        {
                            logger.Warn(message);
                        }
                    }
                    break;

                case LogMessageType.Error:
                    {
                        if (exception != null)
                        {
                            logger.Error(message, exception);
                        }
                        else
                        {
                            logger.Error(message);
                        }
                    }
                    break;

                case LogMessageType.Fatal:
                    {
                        if (exception != null)
                        {
                            logger.Fatal(message, exception);
                        }
                        else
                        {
                            logger.Fatal(message);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region 04.系统请求日志


        #endregion
    }
}
