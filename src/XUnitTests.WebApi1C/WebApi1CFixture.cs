using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApi1C.Client.Services;
using Xunit;

namespace WebApi1C.XUnitTest
{
    public class WebApplicationFactory1C<T>: WebApplicationFactory<T> where T:class
    {
 
        protected override void Dispose(bool disposing)
        {
            var client = Server.CreateClient();
            client.PostAsJsonAsync("/api/v1c77", false).Wait();
            base.Dispose(disposing);
        }

    }

    [CollectionDefinition("Server 1C")]
    public class Connection1C77FixtureCollection : ICollectionFixture<WebApplicationFactory1C<WebApi1C.Server.Startup>>
    { }

}
