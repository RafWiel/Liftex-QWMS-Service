using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace InterProcessCommunication.gTools
{
    [XmlRoot(ElementName = "Configuration", DataType = "string", IsNullable = false)]
    public class gConfiguration
    {
        #region Windows configuration

        [XmlElement(ElementName = "Windows")]
        public WindowsConfiguration Windows { get; set; } = new WindowsConfiguration();  

        public class WindowsConfiguration
        {
            [XmlElement(ElementName = "Window")]
            public List<WindowConfiguration> Items { get; set; } = new List<WindowConfiguration>();           
        }

        public class WindowConfiguration
        {
            [XmlElement]
            public string Name { get; set; }

            [XmlElement]
            public int X { get; set; }

            [XmlElement]
            public int Y { get; set; }

            [XmlElement]
            public int Width { get; set; }

            [XmlElement]
            public int Height { get; set; }

            [XmlElement]
            public int State { get; set; }            
        }

        #endregion

        #region File management        

        [XmlIgnore]
        public string FilePath { get; set; }               

        public virtual void Save() { }

        #endregion                
    }
}
