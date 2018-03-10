using System;
using Htc.Vita.Core.Log;
using log4net;
using log4net.Config;

namespace Htc.Vita.Mod.Desktop.Log4Net
{
    public class LoggerImpl : Logger
    {
        private readonly ILog _log;

        public LoggerImpl(string name) : base(name)
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(name);
        }

        protected override void OnDebug(string tag, string message)
        {
            _log.Debug("" + tag + "() " + message);
        }

        protected override void OnDebug(string tag, string message, Exception exception)
        {
            _log.Debug("" + tag + "() " + message, exception);
        }

        protected override void OnError(string tag, string message)
        {
            _log.Error("" + tag + "() " + message);
        }

        protected override void OnError(string tag, string message, Exception exception)
        {
            _log.Error("" + tag + "() " + message, exception);
        }

        protected override void OnFatal(string tag, string message)
        {
            _log.Fatal("" + tag + "() " + message);
        }

        protected override void OnFatal(string tag, string message, Exception exception)
        {
            _log.Fatal("" + tag + "() " + message, exception);
        }

        protected override void OnInfo(string tag, string message)
        {
            _log.Info("" + tag + "() " + message);
        }

        protected override void OnInfo(string tag, string message, Exception exception)
        {
            _log.Info("" + tag + "() " + message, exception);
        }

        protected override void OnShutdown()
        {
            Console.WriteLine("Shutting down the log4net ...");
            LogManager.Shutdown();
        }

        protected override void OnTrace(string tag, string message)
        {
            // log4net does not have Trace level; use Debug level instead
            _log.Debug("" + tag + "() " + message);
        }

        protected override void OnTrace(string tag, string message, Exception exception)
        {
            // log4net does not have Trace level; use Debug level instead
            _log.Debug("" + tag + "() " + message, exception);
        }

        protected override void OnWarn(string tag, string message)
        {
            _log.Warn("" + tag + "() " + message);
        }

        protected override void OnWarn(string tag, string message, Exception exception)
        {
            _log.Warn("" + tag + "() " + message, exception);
        }
    }
}
