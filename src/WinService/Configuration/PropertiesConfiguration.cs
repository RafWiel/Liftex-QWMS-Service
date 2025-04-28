using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinService.Configuration
{
    public class PropertiesConfiguration
    {
        #if DEBUG

        [XmlElement]
        public int WarehouseId { get; set; } = 1;

        #else

        [XmlElement]
        public int WarehouseId { get; set; } = 11;

        #endif

        [XmlElement]
        public string Operators { get; set; }

        [XmlElement]
        public string Warehouses { get; set; }

        
        [XmlElement]
        public string PmLackSeries { get; set; }

        [XmlElement]
        public int OperatorAccessAttributeId { get; set; }

        [XmlElement]
        public int DocumentSignificanceAttributeId { get; set; }
    }
}
