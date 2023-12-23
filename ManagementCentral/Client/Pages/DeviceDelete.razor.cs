using ManagementCentral.Client.Services;
using ManagementCentral.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ManagementCentral.Client.Pages
{
    [Authorize(Roles = "Admin")]
    public partial class DeviceDelete
    {
        [Inject]
        public IDeviceDataService? DeviceDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? DeviceId { get; set; }

        public Device Device { get; set; } = new Device();

        public string responseData = string.Empty;

        public string ErrorMessage = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (DeviceId.HasValue)
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
                            Device = JsonSerializer.Deserialize<Device>(responseData,
                                options);
                        }
                    }
                    else
                    {
                        ErrorMessage = "Could not get data from API! " +
                            response.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            await base.OnInitializedAsync();
        }

        protected async Task Delete()
        {
            try
            {
                Http.DefaultRequestHeaders.Clear();
                Http.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " +
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Impfa19iIiwic3ViIjoial9rX2IiLCJqdGkiOiJjOGZhOThjZiIsInJvbGUiOiJBZG1pbiIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjIyNjUzIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMjEiLCJodHRwOi8vbG9jYWxob3N0OjUxODkiLCJodHRwczovL2xvY2FsaG9zdDo3MTMxIl0sIm5iZiI6MTcwMzIyOTA4NSwiZXhwIjoxNzExMDkxNDg1LCJpYXQiOjE3MDMyMjkwODYsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.Qu8qHWqieMzwMpO7VFyIwhCVWQUuPPZOPTcfi4IBy8M");
                Http.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await Http.DeleteAsync("/device/delete/"
                    + Device.DeviceId);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo($"/listofdevices");
                }
                else
                {
                    ErrorMessage = "Could not delete Device in API! "
                        + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteError()
        {
            try
            {
                Http.DefaultRequestHeaders.Clear();
                Http.DefaultRequestHeaders.Add("Authorization",
                    "Bearer " +
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Impfa19" +
                    "iIiwic3ViIjoial9rX2IiLCJqdGkiOiJjOGZhOThjZiIsInJvbGUiOiJBZG1pbiI" +
                    "sImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjIyNjUzIiwiaHR0cHM6Ly9sb2NhbGh" +
                    "vc3Q6NDQzMjEiLCJodHRwOi8vbG9jYWxob3N0OjUxODkiLCJodHRwczovL2xvY2F" +
                    "saG9zdDo3MTMxIl0sIm5iZiI6MTcwMzIyOTA4NSwiZXhwIjoxNzExMDkxNDg1LCJ" +
                    "pYXQiOjE3MDMyMjkwODYsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.Qu8qHWqi" +
                    "eMzwMpO7VFyIwhCVWQUuPPZOPTcfi4IBy8M");
                Http.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await Http.DeleteAsync("/device/delete/11");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //NavigationManager.NavigateTo($"/listofdevices");
                }
                else
                {
                    ErrorMessage = "Could not delete Device in API! "
                        + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
