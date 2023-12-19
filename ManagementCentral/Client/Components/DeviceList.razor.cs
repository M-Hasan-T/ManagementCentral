using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace ManagementCentral.Client.Components
{
    public partial class DeviceList
    {
        [Parameter]
        public string ExtraCaption { get; set; } = string.Empty;

        [Inject]
        public IDeviceDataService? DeviceDataService { get; set; }

        public List<Device> DeviceLst { get; set; } = new List<Device>();

        protected override void OnInitialized()
        {
            DeviceLst = DeviceDataService.GetDevices();
            base.OnInitialized();
        }
    }
}
