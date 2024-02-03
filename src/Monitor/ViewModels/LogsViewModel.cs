using InterProcessCommunication.Services;
using MainApplication.Models;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows.Threading;

namespace MainApplication.ViewModels
{
    public class LogsViewModel
    {
        #region Initialization

        public delegate void DataGridEventDelegate();
        public event DataGridEventDelegate UnselectDataGridItemEvent;
        public event DataGridEventDelegate ScrollToLastDataGridItemEvent;        

        public Dispatcher ViewDispatcher { get; set; }
        private ObservableCollection<LogModel> _items = new ObservableCollection<LogModel>();
        public ObservableCollection<LogModel> Items
        {
            get { return _items; }
            set
            {
                if (_items == value)
                    return;

                _items = value;
            }
        }

        private MonitorWcfService _monitorWcfService = new MonitorWcfService();

        #endregion

        #region Events

        private void Service_LogEvent(string message, bool isError)
        {            
            var item = new LogModel
            {
                Timestamp = DateTime.Now,
                Event = message,
                IsError = isError,
            };

            ViewDispatcher.Invoke(() =>
            {
                Items.Add(item);

                if (Items.Count > 1000)
                {
                    //item is about to be deleted, cancel selection
                    UnselectDataGridItemEvent?.Invoke();
                    Items.RemoveAt(0);
                }

                ScrollToLastDataGridItemEvent?.Invoke();
            });
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            _monitorWcfService.MonitorLogEvent += Service_LogEvent;
            _monitorWcfService.Start();                     
        }
        
        public void Cleanup()
        {
            _monitorWcfService.Stop();            
        }

        #endregion
    }
}
