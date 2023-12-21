using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net;

namespace ManagementCentral.Client.Pages
{

    public partial class DeviceAdd
    {
        [Inject]
        public IDeviceDataService DeviceDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Device Device { get; set; } = new Device();

        public string responseData = string.Empty;

        public HttpStatusCode statusCode;

        private async Task HandleValidSubmit()
        {
            var json = JsonSerializer.Serialize(Device);
            var httpContent = new StringContent(json, new MediaTypeHeaderValue("application/json"));
            var response = await Http.PostAsync("/device/add", httpContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseData = await response.Content.ReadAsStringAsync();
            }

            statusCode = response.StatusCode;

            //NavigationManager.NavigateTo($"/listofdevices");
        }
    }
}
