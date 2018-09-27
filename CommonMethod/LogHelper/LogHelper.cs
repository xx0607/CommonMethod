using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHelperLibrary
{
    public class LogHelper : BaseLog
    {
        protected override string LogRootDirectory
        {
            get
            {
                return Path.Combine(System.Environment.CurrentDirectory, "Log");
            }
        }

        private const string ErrLogName = "系统异常";
        private const string SysLogName = "系统日志";
        private const string DebugLogName = "调试信息";
        private const string DbLogName = "数据库日志";

        #region 单例
        private static readonly LogHelper instance = new LogHelper();
        public static LogHelper Instance { get { return instance; } }

        private LogHelper()
        {
            Initialize(new[] { ErrLogName, SysLogName, DebugLogName, DbLogName });
        }

        #endregion

        #region 数据库日志
        /// <summary>
        /// 记录系统调用Sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        public ILog GetDbLogger()
        {
            return GetLogger(DbLogName);
        }
        #endregion

        #region 系统异常
        public void LogError(string msg)
        {
            GetLogger(ErrLogName).Error(msg);
        }

        public void LogError(string msg, Exception e)
        {
            GetLogger(ErrLogName).Error(msg, e);
        }

        /// <summary>
        /// 系统致命错误
        /// </summary>
        public void LogFatal(string msg, Exception e)
        {
            GetLogger(ErrLogName).Fatal(msg, e);
        }
        #endregion

        #region 调试信息
        /// <summary>
        /// 记录系统调试日志
        /// </summary>
        /// <param name="msg"></param>
        public void LogDebug(string msg)
        {
            GetLogger(DebugLogName).Debug(msg);
        }
        #endregion

        #region 系统日志
        /// <summary>
        /// 系统信息日志
        /// </summary>
        /// <param name="msg"></param>
        public void LogSysInfo(string msg)
        {
            GetLogger(SysLogName).Info(msg);
        }

        /// <summary>
        /// 系统错误日志
        /// </summary>
        /// <param name="msg"></param>
        public void LogSysWarn(string msg)
        {
            GetLogger(SysLogName).Warn(msg);
        }
        #endregion
    }
}
