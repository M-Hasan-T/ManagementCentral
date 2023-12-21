using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text.Json;

namespace ManagementCentral.Client.Pages
{
    public partial class DeviceDelete
    {
        [Inject]
        public IDeviceDataService? DeviceDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? DeviceId { get; set; }

        public Device Device { get; set; } = new Device();

        public HttpStatusCode statusCode;

        public string responseData = string.Empty;

        public bool Error = false;
        protected override async Task OnInitializedAsync()
        {
            if (DeviceId.HasValue)
            {
                var response = await Http.GetAsync("/device/" + DeviceId);
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNameCaseInsensitive = true,
                    };

                    responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        Device = JsonSerializer.Deserialize<Device>(responseData, options);
                    }

                }
                else { Error = true; }
                await base.OnInitializedAsync();
            }
            else
            {

            }
        }

        protected async Task Delete()
        {
            var response = await Http.DeleteAsync("/device/delete/" + Device.DeviceId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //NavigationManager.NavigateTo($"/listofdevices");
            }
            else
            {

            }
            statusCode = response.StatusCode;
        }

        protected async Task DeleteError()
        {
            var response = await Http.DeleteAsync("/device/delete/11");
            statusCode = response.StatusCode;
        }
    }
}
