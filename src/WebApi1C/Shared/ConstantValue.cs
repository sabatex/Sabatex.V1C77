using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi1C.Shared
{
    public class ConstantValue
    {
        public ConstantMetadata1C77 Metadata { get; set; }
        public object Value { get; set; }
    }
}
