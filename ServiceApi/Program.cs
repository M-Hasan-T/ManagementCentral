using ManagementCentral.Shared.Domain;

var builder = WebApplication.CreateBuilder(args);

var specOrigin = "MySpecOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: specOrigin, policy =>
    {
        policy.WithOrigins("https://localhost:7026")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/device", () => "Getting a device from API");

app.MapGet("/device/{deviceId}/button/{buttonId}",
    (int deviceId, int buttonId) => $"DeviceId {deviceId} and ButtonId {buttonId}");

app.MapGet("/device/{deviceId}", (int deviceId) =>
{
    var DeviceService = new DeviceService();
    return DeviceService.DeviceList[deviceId].DeviceType;
});

app.UseCors(specOrigin);

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

class DeviceService
{
    public List<Device> DeviceList { get; set; } = new List<Device>();

    public DeviceService()
    {
        {
            DeviceList.Add(new Device()
            {
                DeviceId = 1,
                Location = Location.Sweden,
                Date = DateTime.Now,
                DeviceType = "Sensor",
                Status = Status.online
            });
            DeviceList.Add(new Device()
            {
                DeviceId = 2,
                Location = Location.England,
                Date = DateTime.Now.AddDays(-30),
                DeviceType = "Machine",
                Status = Status.offline
            });
        }
    }
}