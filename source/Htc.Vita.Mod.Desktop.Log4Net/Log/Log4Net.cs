using System;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Util;

namespace Htc.Vita.Mod.Desktop.Log
{
    /// <summary>
    /// Class Log4Net.
    /// </summary>
    public static class Log4Net
    {
        /// <summary>
        /// Class Log4NetLogger.
        /// Implements the <see cref="Core.Log.Logger" />
        /// </summary>
        /// <seealso cref="Core.Log.Logger" />
        public class Logger : Core.Log.Logger
        {
            private readonly ILog _log;

            /// <summary>
            /// Initializes a new instance of the <see cref="Logger" /> class.
            /// </summary>
            /// <param name="name">The name.</param>
            public Logger(string name) : base(name)
            {
                XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()));
                _log = LogManager.GetLogger(
                        Assembly.GetCallingAssembly(),
                        name
                );
            }

            /// <inheritdoc />
            protected override void OnDebug(
                    string tag,
                    string message)
            {
                _log.Debug($"{tag}() {message}");
            }

            /// <inheritdoc />
            protected override void OnDebug(
                    string tag,
                    string message,
                    Exception exception)
            {
                _log.Debug(
                        $"{tag}() {message}",
                        exception
                );
            }

            /// <inheritdoc />
            protected override void OnError(
                    string tag,
                    string message)
            {
                _log.Error($"{tag}() {message}");
            }

            /// <inheritdoc />
            protected override void OnError(
                    string tag,
                    string message,
                    Exception exception)
            {
                _log.Error(
                        $"{tag}() {message}",
                        exception
                );
            }

            /// <inheritdoc />
            protected override void OnFatal(
                    string tag,
                    string message)
            {
                _log.Fatal($"{tag}() {message}");
            }

            /// <inheritdoc />
            protected override void OnFatal(
                    string tag,
                    string message,
                    Exception exception)
            {
                _log.Fatal(
                        $"{tag}() {message}",
                        exception
                );
            }

            /// <inheritdoc />
            protected override void OnInfo(
                    string tag,
                    string message)
            {
                _log.Info($"{tag}() {message}");
            }

            /// <inheritdoc />
            protected override void OnInfo(
                    string tag,
                    string message,
                    Exception exception)
            {
                _log.Info(
                        $"{tag}() {message}",
                        exception
                );
            }

            /// <inheritdoc />
            protected override void OnShutdown()
            {
                Console.Error.WriteLine("Shutting down the log4net ...");
                LogManager.Shutdown();
            }

            /// <inheritdoc />
            protected override void OnTrace(
                    string tag,
                    string message)
            {
                // log4net does not have Trace level; use Debug level instead
                _log.Debug($"{tag}() {message}");
            }

            /// <inheritdoc />
            protected override void OnTrace(
                    string tag,
                    string message,
                    Exception exception)
            {
                // log4net does not have Trace level; use Debug level instead
                _log.Debug(
                        $"{tag}() {message}",
                        exception
                );
            }

            /// <inheritdoc />
            protected override void OnWarn(
                    string tag,
                    string message)
            {
                _log.Warn($"{tag}() {message}");
            }

            /// <inheritdoc />
            protected override void OnWarn(
                    string tag,
                    string message,
                    Exception exception)
            {
                _log.Warn(
                        $"{tag}() {message}",
                        exception
                );
            }
        }

        /// <summary>
        /// Class Log4NetTraceListener.
        /// Implements the <see cref="System.Diagnostics.TraceListener" />
        /// </summary>
        /// <seealso cref="System.Diagnostics.TraceListener" />
        public class TraceListener : System.Diagnostics.TraceListener
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TraceListener"/> class.
            /// </summary>
            public TraceListener()
            {
                LogLog.EmitInternalMessages = false;
            }

            /// <inheritdoc />
            public override void Write(string message)
            {
                Logger.GetInstance(typeof(TraceListener)).Info(message);
            }

            /// <inheritdoc />
            public override void WriteLine(string message)
            {
                Logger.GetInstance(typeof(TraceListener)).Info(message);
            }
        }
    }
}
