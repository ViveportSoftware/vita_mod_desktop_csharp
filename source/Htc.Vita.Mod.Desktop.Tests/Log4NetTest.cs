using System;
using System.Diagnostics;
using Htc.Vita.Core.Log;
using Xunit;

namespace Htc.Vita.Mod.Desktop.Tests
{
    public static class Log4NetTest
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_0_GetInstance_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_0_GetInstance_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_1_Debug()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Debug("Default test debug message");
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_1_Debug_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            logger.Debug("Default test debug message");
            loggerAlt.Debug("Default test debug message in summary", new Exception("summary"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_1_Debug_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            logger.Debug("Default test debug message");
            loggerAlt.Debug("Default test debug message in TestCase");
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_2_Error()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Error("Default test error message");
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_2_Error_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            logger.Error("Default test error message");
            loggerAlt.Error("Default test error message in summary", new Exception("summary"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_2_Error_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            logger.Error("Default test error message");
            loggerAlt.Error("Default test error message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_2_Error_WithTrace()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var traceListenerImpl = new Log.Log4Net.TraceListener();
            Debug.Listeners.Add(traceListenerImpl);
            Trace.Listeners.Add(traceListenerImpl);
            logger.Error("Default test error message");
            Assert.NotNull(logger);
            Debug.WriteLine("Verifying Debug.WriteLine went to log");
            Trace.WriteLine("Verifying Trace.WriteLine went to log");
        }

        [Fact]
        public static void Default_3_Fatal()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Fatal("Default test fatal message");
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_3_Fatal_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            logger.Fatal("Default test fatal message");
            loggerAlt.Fatal("Default test fatal message in summary", new Exception("summary"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_3_Fatal_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            logger.Fatal("Default test fatal message");
            loggerAlt.Fatal("Default test fatal message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_4_Info()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Info("Default test info message");
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_4_Info_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            logger.Info("Default test info message");
            loggerAlt.Info("Default test info message in summary", new Exception("summary"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_4_Info_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            logger.Info("Default test info message");
            loggerAlt.Info("Default test info message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_5_Trace()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Trace("Default test trace message");
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_5_Trace_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            logger.Trace("Default test trace message");
            loggerAlt.Trace("Default test trace message in summary", new Exception("summary"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_5_Trace_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            logger.Trace("Default test trace message");
            loggerAlt.Trace("Default test trace message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_6_Warn()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Warn("Default test warn message");
            Assert.NotNull(logger);
        }

        [Fact]
        public static void Default_6_Warn_WithName()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            logger.Warn("Default test warn message");
            loggerAlt.Warn("Default test warn message in summary", new Exception("summary"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_6_Warn_WithType()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(Log4NetTest));
            Assert.NotNull(loggerAlt);
            logger.Warn("Default test warn message");
            loggerAlt.Warn("Default test warn message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public static void Default_8_Shutdown()
        {
            Logger.Register<Log.Log4Net.Logger>();
            var logger = Logger.GetInstance();
            logger.Shutdown();
        }
    }
}
