using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using Pterodactyl_app.Contracts.Services;
using Pterodactyl_app.Helpers;

using Windows.ApplicationModel;

namespace Pterodactyl_app.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private ElementTheme _elementTheme;
    private string _versionDescription;
    private readonly Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }

    public ICommand SwitchThemeCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        _themeSelectorService = themeSelectorService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
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
        if (localSettings.Values["ServerKey"] == null)
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
