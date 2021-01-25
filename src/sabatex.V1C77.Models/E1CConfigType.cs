using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace sabatex.V1C77.Models
{
    /// <summary>
    /// Поддерживаемые типы конфигураций
    /// </summary>
    [Flags]
    public enum E1CConfigType
    {
        [Description("Укрінтеравтосервіс-Закарпаття 1C7.7")]
        Uaservice = 0x1,
        [Description("Виробництво-Послуги-Бухгалтерія 1C7.7")]
        PUB = 0x2,
        [Description("Інфорсе 1C7.7")]
        Inforce = 0x4,
        [Description("Бухгалтерія для України 1C7.7")]
        Buch = 0x8,
        [Description("Управління торговим підприємством 1C8.2")]
        UTP = 0x10
    }
}
