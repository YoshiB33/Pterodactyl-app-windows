using Microsoft.UI.Xaml.Controls;

using Pterodactyl_app.ViewModels;

namespace Pterodactyl_app.Views;

public sealed partial class SettingsPage : Page
{
    private readonly Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
        ServerAdress.Text = ViewModel.GetServerURL();
    }

    private void AddressChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        localSettings.Values["ServerURL"] = ServerAdress.Text;
    }

    private string? ServerKey;
    private void PasswordBox_PasswordChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ServerKey = ServerKeyBox.Password;
        localSettings.Values["ServerKey"] = ServerKey;
    }
}
