using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHelperLibrary
{
    public abstract class BaseLog
    {
        private string _logPattern = "%date [%t] %-5level  %m%n";

        private string _logRootDirectory = "log";

        private Dictionary<string, ILog> _logDictionary;

        protected virtual string LogPattern
        {
            get
            {
                return this._logPattern;
            }
        }

        protected virtual string LogRootDirectory
        {
            get
            {
                return this._logRootDirectory;
            }
        }

        protected void Initialize(string[] logFileNames)
        {
            if (logFileNames == null)
            {
                throw new ArgumentNullException();
            }
            if (!Directory.Exists(this.LogRootDirectory))
            {
                Directory.CreateDirectory(this.LogRootDirectory);
            }
            this._logDictionary = new Dictionary<string, ILog>();
            for (int i = 0; i < logFileNames.Length; i++)
            {
                string text = logFileNames[i];
                this._logDictionary.Add(text, this.InitializeLog(text));
            }
        }

        protected ILog GetLogger(string logFileName)
        {
            return this._logDictionary[logFileName];
        }

        private ILog InitializeLog(string logFileName)
        {
            ILog logger = LogManager.GetLogger(logFileName);
            Logger logger2 = logger.Logger as Logger;
            if (logger2 != null)
            {
                logger2.AddAppender(this.CreateRollingFileAppender(logFileName));
            }
            Logger logger3 = logger.Logger as Logger;
            if (logger3 != null)
            {
                logger3.Hierarchy.Configured = true;
            }
            return logger;
        }

        private IAppender CreateRollingFileAppender(string logName)
        {
            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = this.LogPattern
            };
            patternLayout.ActivateOptions();
            RollingFileAppender rollingFileAppender = new RollingFileAppender
            {
                Layout = patternLayout,
                LockingModel = new FileAppender.MinimalLock(),
                Name = logName,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = string.Format("\\\\\\\\yyyy-MM-dd\\\\\\\\{0}'.log'", logName),
                StaticLogFileName = false,
                File = string.Format("{0}\\", this.LogRootDirectory)
            };
            rollingFileAppender.ActivateOptions();
            return rollingFileAppender;
        }
    }
}
