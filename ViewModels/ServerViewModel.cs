using CommunityToolkit.Mvvm.ComponentModel;

namespace Pterodactyl_app.ViewModels;

public class ServerViewModel : ObservableRecipient
{
    private readonly Windows.Storage.ApplicationDataContainer localSettings =
    Windows.Storage.ApplicationData.Current.LocalSettings;
    public ServerViewModel()
    {
    }
    public string ServerURL()
    {
        if (localSettings.Values["ServerURL"] is not null)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return localSettings.Values["ServerURL"].ToString();
#pragma warning restore CS8603 // Possible null reference return.
        }
        else
        {
            return "No value set";
        }
    }
}
