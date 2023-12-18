namespace ManagementCentral.Shared.Domain
{
    public enum Status
    {
        online,
        offline,
    }
    public enum Location
    {
        Sweden,
        England,
    }
    public class Device
    {
        Guid DeviceId { get; set; }
        Location Location { get; set; }

        DateTime Date { get; set; }
        string DeviceType { get; set; } = string.Empty;
        Status Status { get; set; }
    }
}
