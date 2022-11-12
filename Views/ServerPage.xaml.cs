using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Pterodactyl_app.Services;
using Pterodactyl_app.ViewModels;
using Pterodactyl_app.core.Helpers;
using Pterodactyl_app.Models.AuthenticateWebsocket;
using System.Text;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;

namespace Pterodactyl_app.Views;

public sealed partial class ServerPage : Page
{
    public ServerViewModel ViewModel
    {
        get;
    }

    public ServerPage()
    {
        ViewModel = App.GetService<ServerViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        var data = (Models.ListAPIModel.Attributes)e.Parameter;
        if (data is not null)
        {
            NameBox.Text = $"{data.name} ";
            ViewModel.AfterData(data, LogBox, StatusBox, NameBox);
        }
        base.OnNavigatedTo(e);
    }
}
