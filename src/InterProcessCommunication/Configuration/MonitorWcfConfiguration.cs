using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterProcessCommunication.Configuration
{
    public class MonitorWcfConfiguration
    {
        public const string EndpointAddress = "net.pipe://localhost/liftex_qwms_monitor";
        public const string ProcessName = "Monitor Liftex QWMS";
    }
}
