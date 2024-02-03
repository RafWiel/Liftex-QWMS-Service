using System.ServiceModel;

namespace InterProcessCommunication.Interfaces
{   
    [ServiceContract]
    public interface IMonitorWcfService
    {        
        [OperationContract]
        void LogEvent(string message, bool isError);
    }
}
