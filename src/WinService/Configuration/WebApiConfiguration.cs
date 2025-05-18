using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinService.Configuration
{
    public class WebApiConfiguration
    {
        #if DEBUG
        
        [XmlElement]
        public string Address { get; set; }  = "http://192.168.1.107:3001";

        #else

        [XmlElement]
        public string Address { get; set; } = "http://192.168.1.10:3001";
        
        #endif
    }
}
