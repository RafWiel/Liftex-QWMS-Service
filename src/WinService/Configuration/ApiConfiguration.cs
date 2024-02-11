using InterProcessCommunication.gTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinService.Configuration
{
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
}
