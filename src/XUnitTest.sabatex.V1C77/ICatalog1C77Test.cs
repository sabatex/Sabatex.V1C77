// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestV1C77
{
    [Collection("Connection 1C77")]
    public class ICatalog1C77Test
    {
        Connection1C77Fixture _connection;
        const string testElementCode = "159024";
        public ICatalog1C77Test(Connection1C77Fixture connection)
        {
            _connection = connection;
        }

        [Fact]
        public void FindByCode()
        {

            var contr =  _connection.V1C77.GlobalContext.CreateObjectCatalog("Контрагенты");
            if (contr.FindByCode(testElementCode))
            {
                if (contr.Code != testElementCode)
                    throw new Exception("Error find code");
            }
        }

    }
}
