using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ManagementCentral.Client.Pages
{
    public partial class DeviceView
    {
        [Inject]
        public IDeviceDataService? DeviceDataService { get; set; }

        [Parameter]
        public int DeviceId { get; set; }

        public Device Device { get; set; } = new Device();

        public string responseData = string.Empty;

        public string ErrorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http.GetAsync("/device/" + DeviceId);
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNameCaseInsensitive = true
                    };

                    responseData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseData))
                    {
                        Device = JsonSerializer.Deserialize<Device>(responseData, options);
                    }
                }
                else
                {
                    ErrorMessage = "Could not read from API!" + response.StatusCode;
                }
            }
            catch (Exception exeption)
            {
                ErrorMessage = exeption.Message;
            }

            await base.OnInitializedAsync();
        }

    }
}
