using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration.Install;
using System.IO;
using System.Windows.Forms;
using WinService;

namespace WinService_Service
{
    class WindowsService : ServiceBase
    {
        private MainService _mainService = new MainService();

        public WindowsService()
        {
            this.ServiceName = Configuration.ServiceName;
            this.EventLog.Log = "Application";

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = false;
            this.CanShutdown = true;
            this.CanStop = true;

            string ep = Application.ExecutablePath;
            string dir = Path.GetDirectoryName(ep);
            Directory.SetCurrentDirectory(dir);
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string arg = args[0].ToLower();

                if (arg.Contains("/install") || arg.Contains("-install"))
                {
                    try
                    {
                        //metoda instaluje usluge, ale takze zmienia typ uruchomienia na automatyczny
                        //w takim wypadku trzeba ja najpierw odinstalowac

                        bool found = false;
                        foreach (ServiceController service in ServiceController.GetServices())
                        {
                            if (service.ServiceName == Configuration.ServiceName)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found == true)
                            ManagedInstallerClass.InstallHelper(new string[] { "/u", String.Format("{0}\\{1}", Environment.CurrentDirectory, Configuration.ExecutableName) });

                        string[] param = { String.Format("{0}\\{1}", Environment.CurrentDirectory, Configuration.ExecutableName) };
                        ManagedInstallerClass.InstallHelper(param);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    Environment.Exit(0);
                }

                if (arg.Contains("/uninstall") || arg.Contains("-uninstall"))
                {
                    try
                    {
                        string[] param = { "/u", String.Format("{0}\\{1}", Environment.CurrentDirectory, Configuration.ExecutableName) };
                        ManagedInstallerClass.InstallHelper(param);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    Environment.Exit(0);
                }

                /*if (arg.Contains("/disable") || arg.Contains("-disable"))
                {
                    try
                    {
                        bool found = false;
                        foreach (ServiceController service in ServiceController.GetServices())
                        {
                            if (service.ServiceName == Configuration.ServiceName)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found == true)
                            ManagedInstallerClass.InstallHelper(new string[] { "/u", String.Format("{0}\\{1}", Environment.CurrentDirectory, Configuration.ExecutableName) });

                        Configuration.StartMode = ServiceStartMode.Disabled;

                        string[] param = new string[] { String.Format("{0}\\{1}", Environment.CurrentDirectory, Configuration.ExecutableName) };
                        ManagedInstallerClass.InstallHelper(param);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    Environment.Exit(0);
                }*/
            }

            Console.WriteLine(String.Format("{0} parameters:", Configuration.ServiceName));
            Console.WriteLine("-install");
            Console.WriteLine("-uninstall");

            ServiceBase.Run(new WindowsService());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            _mainService.Start();
        }

        protected override void OnStop()
        {
            _mainService.Stop();
        }

        protected override void OnShutdown()
        {
            _mainService.Stop();
        }

        /// <summary>
        /// OnPause: Put your pause code here
        /// - Pause working threads, etc.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
        }

        /// <summary>
        /// OnContinue(): Put your continue code here
        /// - Un-pause working threads, etc.
        /// </summary>
        protected override void OnContinue()
        {
            base.OnContinue();
        }

        /// <summary>
        /// OnCustomCommand(): If you need to send a command to your
        ///   service without the need for Remoting or Sockets, use
        ///   this method to do custom methods.
        /// </summary>
        /// <param name="command">Arbitrary Integer between 128 & 256</param>
        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
        }

        /// <summary>
        /// OnPowerEvent(): Useful for detecting power status changes,
        ///   such as going into Suspend mode or Low Battery for laptops.
        /// </summary>
        /// <param name="powerStatus">The Power Broadcast Status
        /// (BatteryLow, Suspend, etc.)</param>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>
        /// OnSessionChange(): To handle a change event
        ///   from a Terminal Server session.
        ///   Useful if you need to determine
        ///   when a user logs in remotely or logs off,
        ///   or when someone logs into the console.
        /// </summary>
        /// <param name="changeDescription">The Session Change
        /// Event that occured.</param>

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }


    }
}
