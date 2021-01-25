using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1C.Shared;

namespace WebApi1C.Client.Shared
{
    public partial class NavMenu : IDisposable
    {
        [Inject] Services.Service1C77 Service1C77 { get; set; }
        
        private bool collapseNavMenu = true;
        private bool service1C77Started = false;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void StateChange(Service1C77State service1C77State)
        {
            if (service1C77Started != service1C77State.IsWorked)
            {
                service1C77Started = service1C77State.IsWorked;
                StateHasChanged();
            }
        }
        protected override void OnInitialized()
        {
            Service1C77.OnState1C77Change += StateChange;
        }
        public void Dispose()
        {
            Service1C77.OnState1C77Change -= StateChange;
        }
    }
}
