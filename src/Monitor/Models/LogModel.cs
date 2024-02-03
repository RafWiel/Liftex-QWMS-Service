using gTools.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class LogModel : gBindableBase
    {
        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get => _timestamp;
            set => Set(ref _timestamp, value);
        }

        private string _event;
        public string Event
        {
            get => _event;
            set => Set(ref _event, value);
        }        

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => Set(ref _isError, value);
        }
    }
}
