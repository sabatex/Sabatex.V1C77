// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.V1C77.Models.Metadata
{
    public class EnumMetadata1C77:ObjectMetadata1C77
    {
        /// <summary>
        /// collection for enum values
        /// </summary>
        public IEnumerable<EnumValueMetadata1C77> Values { get; set; }
    }
}
