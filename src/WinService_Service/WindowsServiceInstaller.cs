using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Collections;
using Microsoft.Win32;

namespace WinService_Service
{
    [RunInstaller(true)]
    public class WindowsServiceInstaller : Installer
    {        
        /// <summary>
        /// Public Constructor for WindowsServiceInstaller.
        /// - Put all of your Initialization code here.
        /// </summary>
        public WindowsServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();
            
            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;
            
            //# Service Information            
            serviceInstaller.DisplayName = Configuration.ServiceName;
            serviceInstaller.Description = Configuration.ServiceDescription;
            serviceInstaller.StartType = Configuration.StartMode;
            
            //# This must be identical to the WindowsService.ServiceBase name
            //# set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = Configuration.ServiceName;

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            /*
            //zezwolenie na wyswietlanie okna
            RegistryKey ckey = Registry.LocalMachine.OpenSubKey(
              @"SYSTEM\CurrentControlSet\Services\" + Configuration.ServiceName, true);
            
            if (ckey != null)
            {
                if (ckey.GetValue("Type") != null)
                    ckey.SetValue("Type", ((int)ckey.GetValue("Type") | 256));
            }*/
        }
    }
}
