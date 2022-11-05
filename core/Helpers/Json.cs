using Newtonsoft.Json;

namespace Pterodactyl_app.Core.Helpers;

public static class Json
{
    public static async Task<T> ToObjectAsync<T>(string value)
    {
        return await Task.Run<T>(() =>
        {
#pragma warning disable CS8603 // Possible null reference return.
            return JsonConvert.DeserializeObject<T>(value);
#pragma warning restore CS8603 // Possible null reference return.
        });
    }

    public static async Task<string> StringifyAsync(object value)
    {
        return await Task.Run<string>(() =>
        {
            return JsonConvert.SerializeObject(value);
        });
    }
}
