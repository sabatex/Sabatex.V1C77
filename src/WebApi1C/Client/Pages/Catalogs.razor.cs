using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
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
    public partial class Catalogs
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] NavigationManager uriHelper { get; set; }
        [Inject] public Service1C77 service1C77 { get; set; }

        [Parameter] public string name { get; set; }
        CatalogMetadata1C77 catalogMetadata;


        string[] canSelectedFieldNames;
        object[] catalogItems;
        string searchString;
        bool error = false;

        protected override async Task OnParametersSetAsync()
        {
            var query = new Uri(uriHelper.Uri).Query;
            var qd = QueryHelpers.ParseQuery(query);
            if (qd.TryGetValue("id",out var value))
            {
                catalogMetadata = (await service1C77.GetMetadata()).Справочники[value.ToString()];
                string queryString = $"api/v1c77/catalogs?catalogName={catalogMetadata.Идентификатор}";

                catalogItems = await Http.GetFromJsonAsync<object[]>(queryString);
                foreach (var item in catalogItems)
                {
                    var it = item;
                }

            }
            else
            {
                error = true;
            }

            //catalogMetadata = _
            //var t = Metadata1C77.Attributes.Where(a => a.Сортировка).Select(s=>s.Идентификатор).ToList();
            //t.Insert(0,"Код");
            //t.Insert(0, "Наименование");
            //canSelectedFieldNames = t.ToArray();
        }
    }
}
