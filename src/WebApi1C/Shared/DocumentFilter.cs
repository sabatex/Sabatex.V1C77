using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi1C.Shared
{
    public class DocumentFilter
    {
        public DateTime? BeginPeriod { get; set; }
        public DateTime? EndPeriod { get; set; }
        public int Top { get; set; } = 50;
        public string MyProperty { get; set; }
    }

}
