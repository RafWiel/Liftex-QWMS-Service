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
        [XmlElement]
        public string Operators { get; set; }

        [XmlElement]
        public string Warehouses { get; set; }

        [XmlElement]
        public int WarehouseId { get; set; } = 1;

        [XmlElement]
        public string PmLackSeries { get; set; }

        [XmlElement]
        public int OperatorAccessAttributeId { get; set; }

        [XmlElement]
        public int DocumentSignificanceAttributeId { get; set; }
    }
}
