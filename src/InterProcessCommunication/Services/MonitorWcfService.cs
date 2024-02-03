using InterProcessCommunication.Configuration;
using InterProcessCommunication.Interfaces;
using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterProcessCommunication.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MonitorWcfService : BaseWcfService, IMonitorWcfService
    {
        public delegate void MonitorLogDelegate(string message, bool isError);
        public event MonitorLogDelegate MonitorLogEvent;        
        
        public void Start()
        {            
            Start("InterProcessCommunication.Interfaces.IMonitorWcfService", MonitorWcfConfiguration.EndpointAddress);         
        }
        
        new public void LogEvent(string message, bool isError)
        {
            MonitorLogEvent?.Invoke(message, isError);
        }
    }
}
