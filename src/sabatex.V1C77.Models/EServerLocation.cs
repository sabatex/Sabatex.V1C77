// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.                                      
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace sabatex.V1C77.Models
{
    public enum EServerLocation
    {
        [Description("Файлова")]
        File,
        [Description("Сервер 1С")]
        Server
    }
}
