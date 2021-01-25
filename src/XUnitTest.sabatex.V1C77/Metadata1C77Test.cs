using sabatex.V1C77;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestV1C77
{
    [Collection("Connection 1C77")]
    public class Metadata1C77Test
    {
        Connection1C77Fixture _connection;
         public Metadata1C77Test(Connection1C77Fixture connection)
        {
            _connection = connection;
        }

         [Fact]
        public void GetConstantMetadata()
        {
            var metadata = MetadataBuilder.GetMetadataConstants(_connection.V1C77);
        }
        [Fact]
        public void GetMetadataCatalogs()
        {
            var metadata = MetadataBuilder.GetMetadataCatalogs(_connection.V1C77);
        }

        [Fact]
        public void GetMetadataEnums()
        {
            var metadata = MetadataBuilder.GetMetadataEnums(_connection.V1C77);
        }
        [Fact]
        public void GetMetadataDocuments()
        {
            var metadata = MetadataBuilder.GetMetadataDocuments(_connection.V1C77);
        }
        [Fact]
        public void GetMetadataCommonDocumentAttributes()
        {
            var metadata = MetadataBuilder.GetMetadataCommonDocumentAttributes(_connection.V1C77);
        }
        [Fact]
        public void GetMetadata()
        {
            var metadata = MetadataBuilder.GetMetadata(_connection.V1C77);
        }


    }
}
