// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using Microsoft.AspNetCore.Mvc.Testing;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi1C.XUnitTest
{
    [Collection("Server 1C")]
    public class V1C77CatalogsTest// : IClassFixture<WebApplicationFactory1C<WebApi1C.Server.Startup>>
    {
        private readonly WebApplicationFactory1C<WebApi1C.Server.Startup> _factory;

        public V1C77CatalogsTest(WebApplicationFactory1C<WebApi1C.Server.Startup> factory)
        {
            _factory = factory;
            var client = _factory.CreateClient();
            client.PostAsJsonAsync("/api/v1c77", true).Wait();
        }
        [Theory]
        [InlineData("Контрагенты", "2235004284")]
        [InlineData("Валюты", "")]
        public async Task GetCatalog(string catalogName,string catalogId)
        {
            //await Start1C77();
            var client = _factory.CreateClient();
            string query = $"/api/v1c77/catalogs?catalogName={catalogName}";
            if (!string.IsNullOrWhiteSpace(catalogId))
                query +=$"&catalogId={catalogId}";
            var response = await client.GetAsync(query);
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            if (!string.IsNullOrWhiteSpace(catalogId))
            {
                var catalogs = await response.Content.ReadFromJsonAsync<Dictionary<string,object>>();
                Assert.False(catalogs.Count <= 0);
            }
            else
                {
                    var catalogs = await response.Content.ReadFromJsonAsync<object[]>();
                    Assert.False(catalogs.Length <= 0);
                }
        }
        [Fact]
        public async Task TestAllCatalogs()
        {
            //await Start1C77();
            var client = _factory.CreateClient();
            var metadata = await client.GetFromJsonAsync<RootMetadata1C77>("/api/v1c77/metadata");
            foreach (var cat in metadata.Справочники.Values)
            {
                try
                {
                    var catalogs = await client.GetFromJsonAsync<object[]>($"/api/v1c77/catalogs?catalogName={cat.Идентификатор}");
                }
                catch (Exception e)
                {
                    throw new Exception($"Error get {cat.Идентификатор} {e.Message}");
                }
            }
        }



    }
}
