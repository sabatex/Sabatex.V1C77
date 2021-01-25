using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApi1C.Client.Services;

namespace WebApi1C.Client.Pages
{
    public partial class Document
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] NavigationManager uriHelper { get; set; }
        [Inject] public Service1C77 service1C77 { get; set; }

        [Parameter] public string name { get; set; }

        DateTime? startDate=null;
        DateTime? endDate=null;

        
        
        DocummentMetadata1C77 documentMetadata;


        string[] canSelectedFieldNames;
        object[] items;
        string searchString;
        bool error = false;

        async Task beginChange(DateTime? value)
        {
            startDate = value;
            await filterChange();
        }

        async Task endChange(DateTime? value)
        {
            endDate = value;
            await filterChange();
        }

        async Task filterChange()
        {
            items = null;
            var queryString = new StringBuilder("api/v1c77/documents?");
            queryString.Append($"documentName={documentMetadata.Идентификатор}");
            if (startDate != null) queryString.Append($"&beginDate={startDate.Value.ToString("yyyy-MM-dd")}");
            if (endDate != null) queryString.Append($"&endDate={endDate.Value.ToString("yyyy-MM-dd")}");
            var qs = queryString.ToString();
            var responce = await Http.GetAsync(queryString.ToString());
            if (responce.IsSuccessStatusCode)
            {
                items = await responce.Content.ReadFromJsonAsync<object[]>();
            }
            else
            {
                error = true;
            }

        }

        protected override async Task OnParametersSetAsync()
        {
            var query = new Uri(uriHelper.Uri).Query;
            var qd = QueryHelpers.ParseQuery(query);
            if (qd.TryGetValue("id",out var value))
            {
                string docName = value.ToString();
                var docMetadata = await service1C77.GetMetadata();
                documentMetadata = docMetadata.Документы[docName];
                string queryString = $"api/v1c77/documents?documentName={documentMetadata.Идентификатор}";
                var responce = await Http.GetAsync(queryString);
                if (responce.IsSuccessStatusCode)
                {
                    items = await responce.Content.ReadFromJsonAsync<object[]>();
                }
                else
                {
                    error = true;
                }

            }
            else
            {
                error = true;
            }
        }
    }
}
