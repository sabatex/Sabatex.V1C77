// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using sabatex.V1C77;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;
using XUnitTest.sabatex.V1C77;

namespace XUnitTestV1C77
{
    [Trait("Order", "")]
    [TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
    public class MetadataV1C77Test:IClassFixture<ConnectionFixture>
    {
        public static IGlobalContext V1C77 { get; private set; }

        public MetadataV1C77Test(ConnectionFixture connection)
        {
            V1C77 = connection.V1C77;
        }

        [Fact,Order(1)]
        public void GetConstantMetadata()
        {
            var metadata = MetadataBuilder.GetMetadataConstants(V1C77);
        }
        [Fact,Order(2)]
        public void GetMetadataCatalogs()
        {
            var metadata = MetadataBuilder.GetMetadataCatalogs(V1C77);
        }

        [Fact,Order(3)]
        public void GetMetadataEnums()
        {
            var metadata = MetadataBuilder.GetMetadataEnums(V1C77);
        }
        [Fact,Order(4)]
        public void GetMetadataDocuments()
        {
            var metadata = MetadataBuilder.GetMetadataDocuments(V1C77);
        }
        [Fact,Order(5)]
        public void GetMetadataCommonDocumentAttributes()
        {
            var metadata = MetadataBuilder.GetMetadataCommonDocumentAttributes(V1C77);
        }
        [Fact,Order(6)]
        public void GetMetadata()
        {
            var rootMetadata1C77 = MetadataBuilder.GetMetadata(V1C77);
        }
        [Fact,Order(7)]
        public void GetMetadataDescriptor()
        {
            var metadata = MetadataBuilder.GetMetadataDescriptor(V1C77);
        }


    }
}
