using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Mod.Desktop.Log4Net;
using Xunit;

namespace Htc.Vita.Mod.Desktop.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void LoggerImpl_0_GetInstance()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_0_GetInstance_WithName()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("summary");
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_0_GetInstance_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_1_Debug()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Debug("Default test debug message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_1_Debug_WithName()
        {
            Logger.Register<LoggerImpl>();
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
        public void LoggerImpl_1_Debug_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Debug("Default test debug message");
            loggerAlt.Debug("Default test debug message in TestCase");
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_2_Error()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Error("Default test error message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_2_Error_WithName()
        {
            Logger.Register<LoggerImpl>();
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
        public void LoggerImpl_2_Error_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Error("Default test error message");
            loggerAlt.Error("Default test error message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_3_Fatal()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Fatal("Default test fatal message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_3_Fatal_WithName()
        {
            Logger.Register<LoggerImpl>();
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
        public void LoggerImpl_3_Fatal_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Fatal("Default test fatal message");
            loggerAlt.Fatal("Default test fatal message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_4_Info()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Info("Default test info message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_4_Info_WithName()
        {
            Logger.Register<LoggerImpl>();
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
        public void LoggerImpl_4_Info_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Info("Default test info message");
            loggerAlt.Info("Default test info message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_5_Trace()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Trace("Default test trace message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_5_Trace_WithName()
        {
            Logger.Register<LoggerImpl>();
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
        public void LoggerImpl_5_Trace_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Trace("Default test trace message");
            loggerAlt.Trace("Default test trace message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_6_Warn()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Warn("Default test warn message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void LoggerImpl_6_Warn_WithName()
        {
            Logger.Register<LoggerImpl>();
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
        public void LoggerImpl_6_Warn_WithType()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Warn("Default test warn message");
            loggerAlt.Warn("Default test warn message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void LoggerImpl_7_Shutdown()
        {
            Logger.Register<LoggerImpl>();
            var logger = Logger.GetInstance();
            logger.Shutdown();
        }
    }
}
