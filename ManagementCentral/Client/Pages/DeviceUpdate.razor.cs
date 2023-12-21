using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace ManagementCentral.Client.Pages
{
    public partial class DeviceUpdate
    {
        [Inject]
        public IDeviceDataService DeviceDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? DeviceId { get; set; }

        public Device Device { get; set; } = new Device();

        protected override void OnInitialized()
        {
            if (DeviceId.HasValue)
            {
                Device = DeviceDataService.GetDevice(DeviceId.Value);
            }
            base.OnInitialized();
        }

        protected async Task HandleValidSubmit()
        {
            DeviceDataService.UpdateDevice(Device);
            NavigationManager.NavigateTo($"/listofdevices");
        }
    }
}
