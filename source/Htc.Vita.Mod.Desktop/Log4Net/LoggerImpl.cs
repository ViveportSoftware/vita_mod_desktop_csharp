using System;
using System.Reflection;
using Htc.Vita.Core.Log;
using log4net;
using log4net.Config;

namespace Htc.Vita.Mod.Desktop.Log4Net
{
    /// <summary>
    /// Class LoggerImpl.
    /// Implements the <see cref="Logger" />
    /// </summary>
    /// <seealso cref="Logger" />
    public class LoggerImpl : Logger
    {
        private readonly ILog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerImpl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LoggerImpl(string name) : base(name)
        {
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
            _log = LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
        }

        /// <inheritdoc />
        protected override void OnDebug(string tag, string message)
        {
            _log.Debug($"{tag}() {message}");
        }

        /// <inheritdoc />
        protected override void OnDebug(string tag, string message, Exception exception)
        {
            _log.Debug($"{tag}() {message}", exception);
        }

        /// <inheritdoc />
        protected override void OnError(string tag, string message)
        {
            _log.Error($"{tag}() {message}");
        }

        /// <inheritdoc />
        protected override void OnError(string tag, string message, Exception exception)
        {
            _log.Error($"{tag}() {message}", exception);
        }

        /// <inheritdoc />
        protected override void OnFatal(string tag, string message)
        {
            _log.Fatal($"{tag}() {message}");
        }

        /// <inheritdoc />
        protected override void OnFatal(string tag, string message, Exception exception)
        {
            _log.Fatal($"{tag}() {message}", exception);
        }

        /// <inheritdoc />
        protected override void OnInfo(string tag, string message)
        {
            _log.Info($"{tag}() {message}");
        }

        /// <inheritdoc />
        protected override void OnInfo(string tag, string message, Exception exception)
        {
            _log.Info($"{tag}() {message}", exception);
        }

        /// <inheritdoc />
        protected override void OnShutdown()
        {
            Console.Error.WriteLine("Shutting down the log4net ...");
            LogManager.Shutdown();
        }

        /// <inheritdoc />
        protected override void OnTrace(string tag, string message)
        {
            // log4net does not have Trace level; use Debug level instead
            _log.Debug($"{tag}() {message}");
        }

        /// <inheritdoc />
        protected override void OnTrace(string tag, string message, Exception exception)
        {
            // log4net does not have Trace level; use Debug level instead
            _log.Debug($"{tag}() {message}", exception);
        }

        /// <inheritdoc />
        protected override void OnWarn(string tag, string message)
        {
            _log.Warn($"{tag}() {message}");
        }

        /// <inheritdoc />
        protected override void OnWarn(string tag, string message, Exception exception)
        {
            _log.Warn($"{tag}() {message}", exception);
        }
    }
}
