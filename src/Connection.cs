using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace sabatex.V1C77
{
    [Serializable]
    public class Connection
    {
        public const EPlatform1C Platform1CV7 = EPlatform1C.V1CEnterprise | EPlatform1C.V77 | EPlatform1C.V77L | EPlatform1C.V77M | EPlatform1C.V77S;
        public const E1CConfigType ConfigType1C77 = E1CConfigType.Uaservice | E1CConfigType.PUB | E1CConfigType.Inforce | E1CConfigType.Buch;
        public Connection()
        {
            PlatformType = EPlatform1C.V77M;
            ServerLocation = EServerLocation.File;
            ConfigType = E1CConfigType.PUB;
            ServerAdress = "Server1C77";
            DataBaseName = "Demo";
            UserName = "admin";
            UserPass = "******";
            UseLocalKey = false;
            Exclusive = false;
            Id = Guid.NewGuid();
        }


        public Guid Id { get; set; }

        public virtual EPlatform1C PlatformType { get; set; }
        public virtual E1CConfigType ConfigType { get; set; }

        public EServerLocation ServerLocation { get; set; }

        public string ServerAdress { get; set; }
        public string DataBaseName { get; set; }

        public string DataBasePath { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public bool UseLocalKey { get; set; }
        public bool Exclusive { get; set; }

        public string StringConnection
        {
            get
            {
                string result = "";
                switch (PlatformType)
                {
                    case EPlatform1C.V82:
                    case EPlatform1C.V83:
                        if (ServerLocation == EServerLocation.Server)
                            result = "srvr=\"" + ServerAdress + "\";ref=\"" + DataBaseName + "\";";
                        else
                            result = "file=\"" + DataBasePath + "\";";
                        result = result + "Usr=\"" + UserName + "\";Pwd=\"" + UserPass + "\";";
                        if (UseLocalKey) return result + "UseHWLicenses=0;"; else return result + "UseHWLicenses=1;";
                    case EPlatform1C.V1CEnterprise:
                    case EPlatform1C.V77:
                    case EPlatform1C.V77L:
                    case EPlatform1C.V77M:
                    case EPlatform1C.V77S:
                        return "/D" + DataBasePath + (Exclusive ? " /M" : "") + ((UserName != "") ? " /N" + UserName : "") + ((UserPass != "") ? " /P" + UserPass : "");
                }
                return result;
            }
        }
    }
}
