﻿using InterProcessCommunication.gTools;
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
    public class WinConfiguration : gConfiguration
    {
        public const string FileName = "webapi_techsam_win.cfg";
        
        [XmlElement]
        public ApiConfiguration Api { get; set; } = new ApiConfiguration();

        [XmlElement]
        public DatabaseConfiguration Database { get; set; } = new DatabaseConfiguration();

        public class ApiConfiguration
        {
            [XmlElement]
            public string KeyServer { get; set; } // = "localhost\\SQLEXPRESS::5000140418";

            [XmlElement]
            public string DatabaseName { get; set; } // = "DEV";

            [XmlIgnore]
            public string User { get; set; } // = "ADMIN";

            [XmlElement(ElementName = "User")]
            public string UserEncrypted
            {
                get
                {
                    return gEnc.Encrypt(User);
                }
                set
                {
                    User = gEnc.Decrypt(value);
                }
            }

            [XmlIgnore]
            public string Password { get; set; }

            [XmlElement(ElementName = "Password")]
            public string PasswordEncrypted
            {
                get
                {
                    return gEnc.Encrypt(Password);
                }
                set
                {
                    Password = gEnc.Decrypt(value);
                }
            }
        }

        public class DatabaseConfiguration
        {
            [XmlElement]
            public string Address { get; set; } // = "localhost\\SQLEXPRESS";

            [XmlElement]
            public string Name { get; set; } // = "ERPXL_DEV";

            [XmlIgnore]
            public string User { get; set; } // = "B2B_service";

            [XmlElement(ElementName = "User")]
            public string UserEncrypted
            {
                get
                {
                    return gEnc.Encrypt(User);
                }
                set
                {
                    User = gEnc.Decrypt(value);
                }
            }

            [XmlIgnore]
            public string Password { get; set; } // = "vWBtuu2k9zzNqVjG";

            [XmlElement(ElementName = "Password")]
            public string PasswordEncrypted
            {
                get
                {
                    return gEnc.Encrypt(Password);
                }
                set
                {
                    Password = gEnc.Decrypt(value);
                }
            }

            //[XmlIgnore]
            //public string DatabaseNumber { get; set; }

            //[XmlElement(ElementName = "DatabaseNumber")]
            //public string DatabaseNumberEncrypted
            //{
            //    get
            //    {
            //        return gEnc.Encrypt(DatabaseNumber);
            //    }
            //    set
            //    {
            //        DatabaseNumber = gEnc.Decrypt(value);
            //    }
            //}
        }

        #region File management       

        public static WinConfiguration Load()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DataSoft", FileName);

            return Load(filePath);
        }

        public static WinConfiguration Load(string filePath)
        {
            var cfg = new WinConfiguration();

            try
            {
                if (File.Exists(filePath) == false)
                    return cfg;

                XmlSerializer xs = new XmlSerializer(typeof(WinConfiguration));
                using (var sr = new StreamReader(filePath))
                {
                    cfg = (WinConfiguration)xs.Deserialize(sr);
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
                XmlSerializer xs = new XmlSerializer(typeof(WinConfiguration));
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