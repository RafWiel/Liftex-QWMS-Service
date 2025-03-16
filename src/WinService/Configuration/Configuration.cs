using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinService.Configuration
{
    [XmlRoot(ElementName = "Configuration", DataType = "string", IsNullable = false)]
    public class Configuration : gConfiguration
    {
        public const string FileName = "qwms_liftex.cfg";
        
        [XmlElement]
        public ApiConfiguration Api { get; set; } = new ApiConfiguration();

        [XmlElement]
        public DatabaseConfiguration Database { get; set; } = new DatabaseConfiguration();

        [XmlElement(ElementName = "Properties")]
        public PropertiesConfiguration Properties { get; set; } = new PropertiesConfiguration();

        [XmlElement]
        public WebApiConfiguration WebApi { get; set; } = new WebApiConfiguration();

        #region File management       

        public static Configuration Load()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DataSoft", FileName);

            return Load(filePath);
        }

        public static Configuration Load(string filePath)
        {
            var cfg = new Configuration();

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
