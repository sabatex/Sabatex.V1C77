using Microsoft.Extensions.Configuration;
using sabatex.V1C77;
using sabatex.V1C77.Models;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest.sabatex.V1C77
{
    public class MetadataFixture : IDisposable
    {
        public RootMetadata1C77 RootMetadata1C77 { get; set; }

        public void Dispose()
        {
            
        }
    }
}
