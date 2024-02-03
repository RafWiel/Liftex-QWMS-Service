using gTools.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class MainWindowModel : gBindableBase
    {        
        private ServiceControllerStatus _serviceStatus;
        public ServiceControllerStatus ServiceStatus
        {
            get => _serviceStatus;
            set
            {
                Set(ref _serviceStatus, value);
                NotifyPropertyChanged("ServiceStatusText");
                NotifyPropertyChanged("IsServiceRunning");
            }
        }

        public string ServiceStatusText
        {
            get
            {
                switch (_serviceStatus)
                {
                    case ServiceControllerStatus.Running:
                        return "Usługa uruchomiona";
                    case ServiceControllerStatus.Stopped:
                        return "Usługa zatrzymana";
                    case ServiceControllerStatus.Paused:
                        return "Usługa wstrzymana";
                    case ServiceControllerStatus.StopPending:
                        return "Zatrzymywanie usługi...";
                    case ServiceControllerStatus.StartPending:
                        return "Uruchamianie usługi...";
                    default:
                        return "Usługa niezainstalowana";
                }
            }        
        }

        private bool _isServiceInstalled;
        public bool IsServiceInstalled
        {
            get => _isServiceInstalled;
            set => Set(ref _isServiceInstalled, value);
        }

        public bool IsServiceRunning
        {
            get => _serviceStatus == ServiceControllerStatus.Running;
        }

        public bool IsServiceStopped
        {
            get => 
                _serviceStatus == ServiceControllerStatus.Paused || 
                _serviceStatus== ServiceControllerStatus.Stopped;
        }

        public bool IsNavigationPanelExpanded
        {
            get => App.Config.Interface.IsNavigationPanelExpanded;
            set
            {
                if (App.Config.Interface.IsNavigationPanelExpanded == value)
                    return;

                App.Config.Interface.IsNavigationPanelExpanded = value;

                App.Config.Save();
                NotifyPropertyChanged();
            }
        }
    }
}
