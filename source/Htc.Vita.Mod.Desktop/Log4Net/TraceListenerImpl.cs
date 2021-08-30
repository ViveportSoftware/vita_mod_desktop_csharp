using System;
using System.Diagnostics;
using Htc.Vita.Core.Log;
using log4net.Util;

namespace Htc.Vita.Mod.Desktop.Log4Net
{
    /// <summary>
    /// Class TraceListenerImpl.
    /// Implements the <see cref="TraceListener" />
    /// </summary>
    /// <seealso cref="TraceListener" />
    [Obsolete("This class is obsoleted. Use Log4Net.TraceListener instead.")]
    public class TraceListenerImpl : TraceListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceListenerImpl"/> class.
        /// </summary>
        public TraceListenerImpl()
        {
            LogLog.EmitInternalMessages = false;
        }

        /// <inheritdoc />
        public override void Write(string message)
        {
            Logger.GetInstance(typeof(TraceListenerImpl)).Info(message);
        }

        /// <inheritdoc />
        public override void WriteLine(string message)
        {
            Logger.GetInstance(typeof(TraceListenerImpl)).Info(message);
        }
    }
}
