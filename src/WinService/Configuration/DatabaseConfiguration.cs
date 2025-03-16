using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinService.Configuration
{
    public class DatabaseConfiguration
    {
        [XmlElement]
        public string Address { get; set; } = "ThinkPad-X13\\SQLExpress";

        [XmlElement]
        public string Name { get; set; } = "ERPXL_DEV_OLD";

        [XmlIgnore]
        public string User { get; set; } = "sa";

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
        public string Password { get; set; } = "Developer1!";

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
}
