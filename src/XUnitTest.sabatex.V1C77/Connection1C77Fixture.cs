using Microsoft.Extensions.Configuration;
using sabatex.V1C77;
using sabatex.V1C77.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestV1C77
{
    public class Connection1C77Fixture:IDisposable
    {
        public  COMObject1C77 V1C77 { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }

        public Connection1C77Fixture()
        {
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .AddUserSecrets<IDocument1C77Test>()
                                                .Build();

            var conection1C77 = new Connection
            {
                DataBasePath = Configuration.GetSection("1C7.7")["dbPath"],
                PlatformType = sabatex.V1C77.Models.EPlatform1C.V77M,
                UserName = Configuration.GetSection("1C7.7")["user"],
                UserPass = Configuration.GetSection("1C7.7")["pass"]

            };

            V1C77 = COMObject1C77.CreateConnection(conection1C77);

        }
        public void Dispose()
        {
            V1C77.Dispose();
        }
    }
    [CollectionDefinition("Connection 1C77")]
    public class Connection1C77FixtureCollection: ICollectionFixture<Connection1C77Fixture>
    { }
}
