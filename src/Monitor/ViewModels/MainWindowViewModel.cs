using MainApplication.Models;
using gTools.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.ViewModels
{
    public class MainWindowViewModel
    {
        private const string ServiceName = "QWMS";
        private System.Timers.Timer _serviceTimer;        

        public MainWindowModel Model { get; private set; } = new MainWindowModel();

        public MainWindowViewModel()
        {
            Model.IsServiceInstalled = ServiceController.GetServices().SingleOrDefault(u => u.ServiceName.Equals(ServiceName)) != null;

            _serviceTimer = new System.Timers.Timer { Interval = 500, Enabled = true };
            _serviceTimer.Elapsed += delegate { GetServiceStatus(); };            
        }        

        public void ExpandPanel()
        {
            Model.IsNavigationPanelExpanded = !Model.IsNavigationPanelExpanded;
        }

        public void StartService()
        {
            _serviceTimer.Interval = 100;

            Task.Run(() =>
            {
                RunServiceCommand("start");
            });            
        }

        public void StopService()
        {
            _serviceTimer.Interval = 100;

            Task.Run(() =>
            {
                RunServiceCommand("stop");
            });
        }

        private void RunServiceCommand(string command)
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    //WorkingDirectory = Path.GetDirectoryName(appFilePath),
                    FileName = "net",
                    Arguments = String.Format("{0} {1}", command, ServiceName)
                };

                p.Start();
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }            
        }

        private void GetServiceStatus()
        {
            try
            {
                if (Model.IsServiceInstalled == false)
                {
                    Model.IsServiceInstalled = ServiceController.GetServices().SingleOrDefault(u => u.ServiceName.Equals(ServiceName)) != null;
                    if (Model.IsServiceInstalled == false)
                    {
                        _serviceTimer.Interval = 5000;
                        return;
                    }
                }

                var sc = new ServiceController(ServiceName);

                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        if (Model.ServiceStatus != ServiceControllerStatus.Running)
                            Model.ServiceStatus = ServiceControllerStatus.Running;
                        else
                            _serviceTimer.Interval = 5000;                        
                        break;
                    case ServiceControllerStatus.Stopped:
                        if (Model.ServiceStatus != ServiceControllerStatus.Stopped)
                            Model.ServiceStatus = ServiceControllerStatus.Stopped;
                        else
                            _serviceTimer.Interval = 5000;
                        break;
                    default:
                        break;
                }

                Model.ServiceStatus = sc.Status;
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }
        }
    }
}
