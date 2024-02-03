using System;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using gTools.Log;
using gTools.Configuration;

namespace MainApplication.Configuration
{
    [XmlRoot(ElementName = "Configuration", DataType = "string", IsNullable = false)]
    public class Configuration : gConfiguration
    {        
        [XmlElement]
        public InterfaceConfiguration Interface { get; set; } = new InterfaceConfiguration();

        public class InterfaceConfiguration
        {
            [XmlElement]
            public bool IsNavigationPanelExpanded { get; set; } = true;
        }

        #region File management       

        public static Configuration Load(string filePath)
        {
            Configuration cfg = new Configuration();

            try
            {
                if (File.Exists(filePath) == false)
                    return cfg;

                XmlSerializer xs = new XmlSerializer(typeof(Configuration));
                using (var sr = new StreamReader(filePath))
                {
                    cfg = (Configuration)xs.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }
            finally
            {
                cfg.FilePath = filePath;
            }

            return cfg;
        }

        public override void Save()
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Configuration));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                using (var sw = new StreamWriter(FilePath, false, Encoding.UTF8))
                {
                    xs.Serialize(sw, this, ns);
                }
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }
        }

        #endregion                
    }
}
