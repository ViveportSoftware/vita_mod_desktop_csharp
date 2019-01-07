using System.Diagnostics;
using Htc.Vita.Core.Log;
using log4net.Util;

namespace Htc.Vita.Mod.Desktop.Log4Net
{
    public class TraceListenerImpl : TraceListener
    {
        public TraceListenerImpl()
        {
            LogLog.EmitInternalMessages = false;
        }

        public override void Write(string message)
        {
            Logger.GetInstance(typeof(TraceListenerImpl)).Info(message);
        }

        public override void WriteLine(string message)
        {
            Logger.GetInstance(typeof(TraceListenerImpl)).Info(message);
        }
    }
}
