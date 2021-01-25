using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WebApi1C.Client.Services;
using WebApi1C.Shared;

namespace WebApi1C.Client.Pages
{
    public partial class Index : IDisposable
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public Service1C77 service1C77 { get; set; }

        string errorMessage = string.Empty;


        private void StateChange(Service1C77State service1C77State)
        {
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            service1C77.OnState1C77Change += StateChange;
        }

        public void Dispose()
        {
            service1C77.OnState1C77Change -= StateChange;
        }
    }
}
