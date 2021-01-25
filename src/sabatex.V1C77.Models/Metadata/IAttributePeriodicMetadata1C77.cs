using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public interface IAttributePeriodicMetadata1C77
    {
        /// <summary>
        /// Периодический - является ли данный реквизит справочника периодическим (Число [0 / 1])
        /// </summary>
        bool Периодический { get; set; }
    }
}
