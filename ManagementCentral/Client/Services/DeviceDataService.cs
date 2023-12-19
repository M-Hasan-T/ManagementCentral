using ManagementCentral.Shared.Domain;

namespace ManagementCentral.Client.Services
{
    public class DeviceDataService : IDeviceDataService
    {
        public static List<Device> DeviceList { get; set; } = new List<Device>();

        public DeviceDataService()
        {
            DeviceList.Add(new Device() { DeviceId = 1, Location = Location.Sweden, Date = DateTime.Now, DeviceType = "Sensor", Status = Status.online });
            DeviceList.Add(new Device() { DeviceId = 2, Location = Location.England, Date = DateTime.Now, DeviceType = "Machine", Status = Status.offline });
            DeviceList.Add(new Device() { DeviceId = 3, Location = Location.Sweden, Date = DateTime.Now, DeviceType = "Sensor", Status = Status.online });
        }

        public List<Device> GetDevices()
        {
            return DeviceList;
        }

        public Device GetDevice(int Id)
        {
            return DeviceList.FirstOrDefault(x => x.DeviceId == Id);
        }

        public void DeleteDevice(int Id)
        {
            var device = DeviceList.FirstOrDefault(x => x.DeviceId == Id);
            if (device != null)
            {
                DeviceList.Remove(device);
            }
        }

        public void UpdateDevice(Device updatedDevice)
        {
            var device = DeviceList.FirstOrDefault(d => d.DeviceId == updatedDevice.DeviceId);

            if (device != null)
            {
                device.Location = updatedDevice.Location;
                device.Date = updatedDevice.Date;
                device.DeviceType = updatedDevice.DeviceType;
                device.Status = updatedDevice.Status;
            }
        }

        public void AddDevice(Device device)
        {
            Random rnd = new Random();
            device.DeviceId = rnd.Next(100000);
            DeviceList.Add(device);
        }
    }
}
