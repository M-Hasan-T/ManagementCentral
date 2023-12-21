using ManagementCentral.Shared.Domain;

namespace DeviceApi.Collections
{
    public static class Devices
    {
        public static List<Device> DeviceList = new List<Device>()
        {
            new()
            {
                DeviceId = 1, Location = Location.Sweden, Date = DateTime.Now,
                DeviceType = "Sensor", Status = Status.online
            },
            new()
            {
                DeviceId = 2, Location = Location.England, Date = DateTime.Now.AddDays(-30),
                DeviceType = "Machine", Status = Status.offline
            },
            new()
            {
                DeviceId = 3, Location = Location.England, Date = DateTime.Now.AddDays(-130),
                DeviceType = "Sensor", Status = Status.offline
            },
            new()
            {
                DeviceId = 4, Location = Location.Sweden, Date = DateTime.Now.AddDays(-230),
                DeviceType = "Sensor", Status = Status.online
            },
            new()
            {
                DeviceId = 5, Location = Location.England, Date = DateTime.Now.AddDays(-330),
                DeviceType = "Sensor", Status = Status.online
            },
        };
    }
}
