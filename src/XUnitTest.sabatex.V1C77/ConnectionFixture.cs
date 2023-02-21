using Microsoft.Extensions.Configuration;
using sabatex.V1C77;
using sabatex.V1C77.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTest.sabatex.V1C77
{
    public class ConnectionFixture : IDisposable
    {
        public IGlobalContext V1C77 { get; private set; }
        public ConnectionFixture()
        {
            var Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .Build();

            var conection1C77 = new Connection
            {
                DataBasePath = Configuration.GetSection("1C7.7")["dbPath"],
                PlatformType = EPlatform1C.V77M,
                UserName = Configuration.GetSection("1C7.7")["user"],
                UserPass = Configuration.GetSection("1C7.7")["pass"]

            };

            V1C77 = COMObject1C77.CreateConnection(conection1C77).GlobalContext;


        }
        public void Dispose()
        {
            V1C77.Dispose(); 
        }
    }
}
