// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace sabatex.V1C77.Models
{
    [Flags]
    public enum EPlatform1C
    {
        [Description("1C 7.7 (незалежний ключ)")]
        V1CEnterprise=0x1,
        [Description("1C 7.7 (залежний ключ)")]
        V77=0x2,
        [Description("1C 7.7 (SQL версія)")]
        V77S=0x4,
        [Description("1C 7.7 (локальна версія)")]
        V77L=0x8,
        [Description("1C 7.7 (мережева весія)")]
        V77M=0x10,
        [Description("1C 8.2")]
        V82=0x20,
        [Description("1C 8.3")]
        V83=0x40
    }
}
