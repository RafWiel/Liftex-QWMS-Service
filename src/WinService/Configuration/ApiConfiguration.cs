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
        public string KeyServer { get; set; }  = "ThinkPad-X13\\SQLExpress::5000140418";

        [XmlElement]
        public string DatabaseName { get; set; }  = "DEV_OLD";

        [XmlIgnore]
        public string User { get; set; }  = "ADMIN";

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
