using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace ManagementCentral.Client.Components
{
    public partial class DeviceList
    {
        [Parameter]
        [EditorRequired]
        public string ExtraCaption { get; set; } = string.Empty;

        [Inject]
        public IDeviceDataService? DeviceDataService { get; set; }

        public List<Device> DeviceLst { get; set; } = new List<Device>();
        public string responseData = string.Empty;
        public bool Error = false;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //var authState = await authenticationStateTask;
            //if (!authState.User.IsInRole("Admin"))
            //{
            //    return;
            //}

            var response = await Http.GetAsync("/devices");
            if (response.IsSuccessStatusCode)
            {
                responseData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true,
                };

                DeviceLst = (List<Device>)JsonSerializer
                    .Deserialize<IEnumerable<Device>>(responseData, options);
            }
            else { Error = true; }

            await base.OnInitializedAsync();
        }
    }
}
