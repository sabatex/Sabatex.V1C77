using Microsoft.AspNetCore.Components;
using MudBlazor;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using WebApi1C.Shared;

namespace WebApi1C.Client.Services
{
    public class Service1C77:IDisposable
    {
        private readonly HttpClient Http;
        private readonly ISnackbar Snackbar;
        Timer timer;        
        RootMetadata1C77 metadata;
        Service1C77State serviceState;
        public Service1C77State State=>serviceState;
        public event Action<Service1C77State> OnState1C77Change;


        public Service1C77(HttpClient http,ISnackbar snackbar)
        {
            Http = http;
            Snackbar = snackbar;
            timer = new Timer(elapsedTimer, null, 1000, 1000);
            serviceState = new Service1C77State { IsDoStarted = false, IsWorked = false, TimeWorked = 0 };
        }
        public async Task<RootMetadata1C77> GetMetadata(bool fresh = false) 
        {
                if (metadata != null && !fresh) return metadata;
                metadata = await Http.GetFromJsonAsync<RootMetadata1C77>("api/v1c77/metadata");
                return metadata;
        }
        void setState(Service1C77State state)
        {
            serviceState.IsWorked = state.IsWorked;
            serviceState.IsDoStarted = state.IsDoStarted;
            serviceState.TimeWorked = state.TimeWorked;
            if (state.LastError != string.Empty)
            {
                Task.Run(()=> Snackbar.Add(state.LastError, Severity.Error));
            }
            serviceState.LastError = state.LastError;
            OnState1C77Change?.Invoke(serviceState);
        }
        private void elapsedTimer(object state)
        {
            Task.Run(async () =>
            {
                var serviceState = await Http.GetFromJsonAsync<Service1C77State>("api/v1c77/state");
                setState(serviceState);
            });
        }

        public bool CanDoStart => !serviceState.IsDoStarted && !serviceState.IsWorked;
        public bool CanDoStop => !serviceState.IsDoStarted && serviceState.IsWorked;
        public async Task Start()
        {
            if (!serviceState.IsDoStarted && !serviceState.IsWorked)
                await Http.PostAsJsonAsync("api/v1c77", true);
        }
        public async Task Stop()
        {
            if (serviceState.IsWorked || serviceState.IsDoStarted)
                await Http.PostAsJsonAsync("api/v1c77", false);
        }

        public void Dispose()
        {
            if (timer != null) timer.Dispose();
        }
    }
}
