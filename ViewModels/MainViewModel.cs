using CommunityToolkit.Mvvm.ComponentModel;

namespace Pterodactyl_app.ViewModels;

public class MainViewModel : ObservableRecipient
{
    private readonly Windows.Storage.ApplicationDataContainer localSettings =
    Windows.Storage.ApplicationData.Current.LocalSettings;
    public MainViewModel()
    {
    }
    public string GetServerURL()
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
    public string GetServerKey()
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
}
