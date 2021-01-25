using Microsoft.AspNetCore.Components;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApi1C.Client.Services;

namespace WebApi1C.Client.Pages
{
    public partial class Documents
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] NavigationManager uriHelper { get; set; }
        [Inject] public Service1C77 service1C77 { get; set; }

        DocummentMetadata1C77[] values;
        protected override async Task OnParametersSetAsync()
        {
            var metadata = await service1C77.GetMetadata();
            values = metadata.Документы.Values.ToArray();
        }
 
    }
}
