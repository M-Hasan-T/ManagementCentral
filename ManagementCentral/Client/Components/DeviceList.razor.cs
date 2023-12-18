using ManagementCentral.Shared.Domain;

namespace ManagementCentral.Client.Components
{
    public partial class DeviceList
    {

        public List<Device> DeviceLst { get; set; } = new List<Device>();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            DeviceLst.Add(new Device()
            {
                DeviceId = Guid.NewGuid(),
                Location = Location.Sweden,
                Date = new DateTime(),
                DeviceType = "DeviceType1",
                Status = Status.offline
            });

            DeviceLst.Add(new Device()
            {
                DeviceId = Guid.NewGuid(),
                Location = Location.England,
                Date = new DateTime(),
                DeviceType = "DeviceType2",
                Status = Status.online
            });
        }
    }
}
