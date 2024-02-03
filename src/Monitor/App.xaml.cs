using gTools.Log;
using gTools.Misc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MainApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Initialization

        public static Configuration.Configuration Config { get; set; }

        private Mutex _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            if (gMisc.IsApplicationRunning("jscwa_1137", ref _mutex))
            {
                _mutex = null;

                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Application.Current.Shutdown();

                return;
            }

            base.OnStartup(e);

            GC.KeepAlive(_mutex);

            //init log
            gLog.FileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            //init config
            Config = Configuration.Configuration.Load(String.Format("{0}.cfg", Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)));
        }

        #endregion      

        #region Events

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_mutex != null)
                _mutex.ReleaseMutex();
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            gLog.Error(e.Exception.ToString());
        }

        #endregion  

        #region Methods

        public static void WpfDoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }

        #endregion
    }
}
