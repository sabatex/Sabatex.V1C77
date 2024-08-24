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

    public  class Catalog1C77Test:IClassFixture<MetadataFixture>,IClassFixture<ConnectionFixture>
    {
        const string testElementCode = "159024";
        public IGlobalContext V1C77 { get; private set; }
        public RootMetadata1C77 RootMetadata1C77 { get; private set; }

        public Catalog1C77Test(MetadataFixture metadata,ConnectionFixture connection)
        {
            V1C77 = connection.V1C77;
            if (metadata.RootMetadata1C77 == null)
                metadata.RootMetadata1C77 = MetadataBuilder.GetMetadata(V1C77);
            RootMetadata1C77 = metadata.RootMetadata1C77;
        }

        ICatalog1C77 findByCode(string catalogName, string code)
        {
            var result = V1C77.GlobalContext.CreateObjectCatalog(catalogName);
            if (result.FindByCode(testElementCode))
            {
                if (result.Code != testElementCode)
                    throw new Exception("Error find code");
            }
            return result;
        }
        
        [Fact, Order(0)]
        public void Initial()
        {

        }


        [Fact,Order(8)]
        public void FindByCode()
        {
            var result = findByCode("Контрагенты",testElementCode);

        }
        [Fact,Order(9)]
        public void SerializeJSON()
        {
            var result = findByCode("Контрагенты", testElementCode);
            string s = RootMetadata1C77.SerializeJSON(result,"Контрагенты");
        }

    }
}
