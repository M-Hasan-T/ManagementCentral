using ManagementCentral.Shared.Domain;

namespace DeviceApi.Endpoints
{
    public static class Devices
    {
        public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
        {
            var devices = routes.MapGroup("").RequireAuthorization();

            devices.MapGet("/device", () =>
            {
                Results.Ok("Simple text from API");
            })
            .RequireAuthorization(policy =>
            {
                policy.RequireRole("Admin");
            });

            devices.MapGet("/devices", () =>
            {
                if (Collections.Devices.DeviceList != null &&
                Collections.Devices.DeviceList.Count > 0)
                {
                    return Results.Ok(Collections.Devices.DeviceList);
                }
                else
                {
                    return Results.StatusCode(5); // 5 = does not exist
                }
            }).AllowAnonymous();

            devices.MapGet("/device/{DeviceId}",
               (int DeviceId) =>
               {
                   var device = Collections.Devices.DeviceList
                       .FirstOrDefault(device => device.DeviceId == DeviceId);

                   if (device != null)
                   {
                       return Results.Ok(device);
                   }
                   else
                   {
                       return Results.StatusCode(1); // 1 = not found
                   }

               }).AllowAnonymous();

            devices.MapPost("/device/add", (Device device) =>
            {
                Random rnd = new Random();
                device.DeviceId = rnd.Next(100000);
                Collections.Devices.DeviceList.Add(device);
                return Results.Ok("Add Ok");
            }).RequireAuthorization(policy =>
            {
                policy.RequireRole("Admin");
            });

            devices.MapPut("/device/edit/{DeviceId}", (int DeviceId, Device device) =>
            {
                Device currentDevice = Collections.Devices.DeviceList
                .FirstOrDefault(device => device.DeviceId == DeviceId);
                if (currentDevice != null)
                {
                    currentDevice.Location = device.Location;
                    currentDevice.Status = device.Status;
                    currentDevice.DeviceType = device.DeviceType;
                    currentDevice.Date = device.Date;
                    return Results.Ok("Edit Ok");
                }
                else
                {
                    return Results.StatusCode(3); // 3 = could not add
                }
            });
            devices.MapDelete("/device/delete/{DeviceId}", (int DeviceId) =>
            {
                var Device = Collections.Devices.DeviceList
                .FirstOrDefault(device => device.DeviceId == DeviceId);
                if (Device != null)
                {
                    Collections.Devices.DeviceList.Remove(Device);
                    return Results.Ok("Delete Ok"); // 0 = ok
                }
                else
                {
                    return Results.StatusCode(4); // 4 = could not delete
                }
            }).RequireAuthorization(policy =>
            {
                policy.RequireRole("Admin");
            });
        }
    }
}
