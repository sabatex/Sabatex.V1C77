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

    public partial class DocumentV1C77Test:IClassFixture<ConnectionFixture>,IClassFixture<MetadataFixture>
    {
        public IGlobalContext V1C77 { get; private set; }
        public RootMetadata1C77 RootMetadata1C77 { get; private set; }

        public DocumentV1C77Test(ConnectionFixture connection, MetadataFixture metadata)
        {
            V1C77 = connection.V1C77;
            if (metadata.RootMetadata1C77 == null)
                metadata.RootMetadata1C77 = MetadataBuilder.GetMetadata(V1C77);
            RootMetadata1C77 = metadata.RootMetadata1C77;

        }
        [Fact, Order(0)]
        public void Initial()
        {

        }


        IDocument1C77 findByCode(string code,DateTime date)
        {
            var doc = V1C77.GlobalContext.CreateObjectDocument("Счет");
            if (doc.FindByNum(code, date))
            {
                if (doc.DocNum != code)
                    throw new Exception("Error find code");

            }
            return doc;
        }

        [Fact,Order(10)]
        public void DocumentFindByCode()
        {
            string docNumber = "ТО-0000001";
            DateTime docDatePeriod = DateTime.Parse("01.01.2020");
            var doc =  V1C77.GlobalContext.CreateObjectDocument("Счет");
            if (doc.FindByNum(docNumber, docDatePeriod))
            {
                if (doc.DocNum != docNumber)
                    throw new Exception("Error find code");
                var dataDoc = doc.DocDate;
                var currentLine = doc.LineNum;
                var kind = doc.Kind;
                var kindPresent = doc.KindPresent;
                var curs = doc.GetAttrib<double>("Курс");
                doc.SetAttrib("Курс", curs + 1);
                if (doc.GetAttrib<double>("Курс") != curs + 1)
                    throw new Exception("Not set attribute");
                var lines = doc.LinesCnt;
            }
            else throw new Exception($"Not find doc number {docNumber} in period {docDatePeriod}");
        }
        [Fact, Order(11)]
        public void JSONSerializeTest()
        {
               var doc = findByCode("ТО-0000001", DateTime.Parse("01.01.2020"));
            var s = RootMetadata1C77.SerializeJSON(doc, "Счет");
            var t = 10;
        }
    }
}
