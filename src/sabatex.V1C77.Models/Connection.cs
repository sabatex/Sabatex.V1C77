// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
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

namespace sabatex.V1C77.Models
{
    [Serializable]
    public class Connection
    {
        public const EPlatform1C Platform1CV7 = EPlatform1C.V1CEnterprise | EPlatform1C.V77 | EPlatform1C.V77L | EPlatform1C.V77M | EPlatform1C.V77S;
        public Connection()
        {
            PlatformType = EPlatform1C.V77M;
            DataBasePath = @"C:\";
            UserName = "admin";
            UserPass = "******";
            Exclusive = false;
            Id = Guid.NewGuid();
        }


        public Guid Id { get; set; }

        public virtual EPlatform1C PlatformType { get; set; }

        public string DataBasePath { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public bool Exclusive { get; set; }

        public string StringConnection
        {
            get
            {
                string result = "";
                switch (PlatformType)
                {
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
