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
        [XmlElement]
        public string Address { get; set; }  = "http://ThinkPad-X13:3001";        
    }
}
