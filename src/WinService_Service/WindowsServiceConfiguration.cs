using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace WinService_Service
{
    public static class Configuration
    {
        public const string ServiceName = "QWMS";
        public const string ServiceDescription = "DataSoft QWMS";
        public const string ExecutableName = "WinServiceApp.exe";
        public static ServiceStartMode StartMode = ServiceStartMode.Automatic;
    }
}
