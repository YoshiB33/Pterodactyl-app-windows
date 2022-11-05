namespace Pterodactyl_app.Services;

using System.Net.Http;
using System.Net.Http.Headers;
using Pterodactyl_app.Contracts.Services;

public sealed class APIService : IAPIService
{
    private static readonly Windows.Storage.ApplicationDataContainer localSettings =
    Windows.Storage.ApplicationData.Current.LocalSettings;
    public static string GetServerURL()
    {
        if (localSettings.Values["ServerURL"] == null)
        {
            return "No ServerURL set.";
        }
        else
        {
#pragma warning disable CS8603 // Possible null reference return.
            return localSettings.Values["ServerURL"].ToString();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
    public static string GetServerKey()
    {
        if (localSettings.Values["Serverkey"] == null)
        {
            return "No ServerKey set.";
        }
        else
        {
#pragma warning disable CS8603 // Possible null reference return.
            return localSettings.Values["ServerKey"].ToString();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
    public static HttpClient Client { get; set; } = new HttpClient();

    public static void InitAPISevice()
    {
        if (GetServerURL() != "No ServerURL set." || GetServerKey() != "No ServerKey set.")
        {
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GetServerKey());
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }
        else
        {
            throw new ArgumentException("The APIService culd not be initialized");
        }
    }
}
