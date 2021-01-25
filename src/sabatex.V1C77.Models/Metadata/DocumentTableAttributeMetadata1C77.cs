using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public class DocumentTableAttributeMetadata1C77:AttributeMetadata1C77
    {
        /// <summary>
        /// признак наличия итога по колонке для данного реквизита табличной части документа (Число ["0" / "1"])
        /// </summary>
        public bool ИтогПоКолонке { get; set; }
    }
}
