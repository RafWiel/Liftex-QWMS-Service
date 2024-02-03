using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinService.Models;

namespace WinService.Services
{
    public class BaseService
    {
        #region Initialization

        public delegate void LogDelegate(string message);
        public event LogDelegate LogEvent;
        public event LogDelegate LogError;
               
        #endregion        

        #region Methods

        public virtual void Start()
        {            
        }

        public virtual void Stop()
        {            
        }

        protected void InvokeLogError(string message)
        {
            LogError?.Invoke(message);
        }

        protected void InvokeLogEvent(string message)
        {
            LogEvent?.Invoke(message);
        }        

        #endregion
    }
}
