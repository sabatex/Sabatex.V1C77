using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestV1C77
{
    [Collection("Connection 1C77")]
    public class IDocument1C77Test
    {
        Connection1C77Fixture _connection;
         public IDocument1C77Test(Connection1C77Fixture connection)
        {
            _connection = connection;
        }

        [Fact]
        public void FindByCode()
        {
            string docNumber = "ТО-0000001";
            DateTime docDatePeriod = DateTime.Parse("01.01.2020");
            var doc =  _connection.V1C77.GlobalContext.CreateObjectDocument("Счет");
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
                // test select
                var bw = doc.BackwardOrder();
                var nbw = doc.BackwardOrder(!bw);
                var bw2 = doc.BackwardOrder();
                var lines = doc.LinesCnt;
            }
            else throw new Exception($"Not find doc number {docNumber} in period {docDatePeriod}");
        }

    }
}
