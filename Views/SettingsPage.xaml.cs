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
        ServerAdress.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, ViewModel.GetServerURL());
    }

    private string? ServerURL;
    private void RichEditBox_TextChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ServerAdress.Document.GetText(Microsoft.UI.Text.TextGetOptions.AdjustCrlf, out ServerURL);
        localSettings.Values["ServerURL"] = ServerURL;
    }

    private string? ServerKey;
    private void PasswordBox_PasswordChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ServerKey = ServerKeyBox.Password;
        localSettings.Values["ServerKey"] = ServerKey;
    }
}
