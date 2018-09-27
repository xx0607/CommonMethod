using LogHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.Instance.LogDebug("Kafka发送完成");
            LogHelper.Instance.LogError("Kafka发送完成");
            LogHelper.Instance.LogSysInfo("Kafka发送完成");
            LogHelper.Instance.LogSysWarn("Kafka发送完成");
            LogHelper.Instance.LogDebug("Kafka发送完成");
        }
    }
}
