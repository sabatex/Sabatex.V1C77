// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using Microsoft.AspNetCore.Mvc.Testing;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApi1C.Shared;
using Xunit;

namespace WebApi1C.XUnitTest
{
    [Collection("Server 1C")]
    public class V1C77ApiTest//: IClassFixture<WebApplicationFactory1C<WebApi1C.Server.Startup>>
    {
        private readonly WebApplicationFactory1C<WebApi1C.Server.Startup> _factory;
       
        public V1C77ApiTest(WebApplicationFactory1C<WebApi1C.Server.Startup> factory)
        {
            _factory = factory;
            var client = _factory.CreateClient();
            client.PostAsJsonAsync("/api/v1c77", true).Wait();
        }


        [Fact]
        public async Task GetState()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/v1c77/state");
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            var state = await response.Content.ReadFromJsonAsync<Service1C77State>();
            if (state.IsWorked)
            {
                Assert.False(state.IsDoStarted);
            }
            else
            {
                Assert.Equal(0.0,state.TimeWorked);
            }
        }


        private async Task<bool> isStarted1C77()
        { 
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/v1c77/state");
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            var state = await response.Content.ReadFromJsonAsync<Service1C77State>();
            return state.IsWorked;
        }

        [Fact]
        public async Task GetMetadata()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/v1c77/metadata");
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            var metadata = await response.Content.ReadFromJsonAsync<RootMetadata1C77>();
            Assert.False(string.IsNullOrWhiteSpace(metadata.Идентификатор));

        }



    }
}
