using InterProcessCommunication.Configuration;
using InterProcessCommunication.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterProcessCommunication.Clients
{
    public class MonitorWcfClient 
    {        
        private ChannelFactory<IMonitorWcfService> CreateChannelFactory()
        {
            var binding = new NetNamedPipeBinding
            {
                MaxBufferSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
            };
            
            var endpoint = new EndpointAddress(MonitorWcfConfiguration.EndpointAddress);

            return new ChannelFactory<IMonitorWcfService>(binding, endpoint);
        }     

        private void LogEvent(string message, bool isError)
        {
            if (Process.GetProcessesByName(MonitorWcfConfiguration.ProcessName).Length == 0)
                return;

            using (var channelFactory = CreateChannelFactory())
            {
                var client = channelFactory.CreateChannel();

                client.LogEvent(message, isError);

                ((IClientChannel)client).Close();
            }                            
        }

        public void LogEvent(string message)
        {
            LogEvent(message, false);
        }

        public void LogError(string message)
        {
            LogEvent(message, true);
        }
    }
}
