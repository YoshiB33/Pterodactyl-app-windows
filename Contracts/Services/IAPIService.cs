using System.Net.Http.Headers;

namespace Pterodactyl_app.Contracts.Services;
internal interface IAPIService
{
    public static HttpClient Client { get; set; } = new HttpClient();
    public void InitAPISevice()
    {
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}
