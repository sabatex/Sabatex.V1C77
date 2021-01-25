using Microsoft.AspNetCore.Components;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi1C.Client.Services;
using WebApi1C.Shared;

namespace WebApi1C.Client.Pages
{
    public partial class Constants
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public Service1C77 service1C77 { get; set; }
        Dictionary<string,object> constantValues;
        Dictionary<string,ConstantMetadata1C77> constantMetadata;

        protected override async Task OnParametersSetAsync()
        {
            var metadata = await service1C77.GetMetadata();
            constantMetadata = metadata.Константы;
            constantValues = await Http.GetFromJsonAsync<Dictionary<string, object>>("api/v1c77/constants");
        }
    }
}
