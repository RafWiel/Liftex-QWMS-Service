
using InterProcessCommunication.Configuration;
using InterProcessCommunication.Interfaces;
using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace InterProcessCommunication.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BaseWcfService
    {
        public delegate void LogDelegate(string message);
        public event LogDelegate LogEvent;
        public event LogDelegate LogError;

        private ServiceHost _serviceHost;

        protected void Start(string contractName, string endpointAddress)
        {
            var binding = new NetNamedPipeBinding
            {
                MaxBufferSize = 2147483647,
                MaxBufferPoolSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
            };

            try
            {
                _serviceHost = new ServiceHost(this);

                _serviceHost.AddServiceEndpoint(contractName, binding, endpointAddress);
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }
        }

        public void Stop()
        {
            _serviceHost.Close();
        }

        protected void InvokeLogEvent(string message)
        {
            LogEvent?.Invoke(message);
        }

        protected void InvokeLogError(string message)
        {
            LogError?.Invoke(message);
        }
    }
}
