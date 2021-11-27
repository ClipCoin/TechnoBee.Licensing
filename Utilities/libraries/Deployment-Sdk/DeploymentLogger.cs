using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoBee.Licensing.Utilities.Deployment_Sdk {
    public class DeploymentLogger {
        private Logger _logger;

        public DeploymentLogger() {
            _logger = LogManager.GetCurrentClassLogger();
            Initialize();
        }
        private void Initialize() {
            if (LogManager.Configuration == null) {
                var target = new FileTarget("system");
                var rule = new LoggingRule("*", LogLevel.Trace, target);
                _logger.Factory.Configuration = new LoggingConfiguration();
                _logger.Factory.Configuration.AddTarget(target);
                _logger.Factory.Configuration.LoggingRules.Add(rule);
                target.FileName = "system.log";
                LogManager.ReconfigExistingLoggers();
            }
        }

        public void SetLogPath(string logFilePath) {
            var target = LogManager.Configuration.FindTargetByName("log");
            var fileTarget = target == null ? new FileTarget("log") : (FileTarget)target;
            fileTarget.FileName = logFilePath;
            LogManager.ReconfigExistingLoggers();
        }

        public void Log(string message, MessageType type) {
            var logLevel = GetLogLevel(type);
            _logger.Log(logLevel, message);
        }

        public void Log(Exception ex, MessageType type) {
            var logLevel = GetLogLevel(type);
            _logger.Log(logLevel, ex);
        }

        private LogLevel GetLogLevel(MessageType type) {
            LogLevel level = null;
            switch (type) {
                case MessageType.Message:
                    level = LogLevel.Info;
                    break;

                case MessageType.Warning:
                    level = LogLevel.Warn;
                    break;

                case MessageType.Error:
                    level = LogLevel.Error;
                    break;

                default:
                    level = LogLevel.Info;
                    break;
            }
            return level;
        }
    }
}
