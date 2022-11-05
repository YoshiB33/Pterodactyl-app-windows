using System.Text.Json;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Pterodactyl_app.Services;
using Pterodactyl_app.ViewModels;

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

    private async void AfterData(Models.ListAPIModel.Attributes data)
    {
        try
        {
            var response = await APIService.Client.GetAsync($"{ViewModel.ServerURL()}/api/client/servers/{data.uuid}/resources");
            var json = await JsonSerializer.DeserializeAsync<Models.ResourcesAPIModel.Rootobject>(response.Content.ReadAsStream());
            if (json is not null && json.attributes is not null)
            {
                if (json.attributes.current_state == "offline")
                {
                    StatusBox.Text = "Offline";
                }
                if (json.attributes.current_state == "stopping")
                {
                    StatusBox.Text = "Stopping";
                    AfterData(data);
                }
                if (json.attributes.current_state == "starting")
                {
                    StatusBox.Text = "Starting";
                    StatusBox.Foreground = new SolidColorBrush(Colors.Yellow);
                    AfterData(data);
                }
                if (json.attributes.current_state == "running")
                {
                    StatusBox.Text = "Online";
                    StatusBox.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }
        catch (Exception ex)
        {
            NameBox.Text += $"{ex.Message} {ViewModel.ServerURL()}/api/client/servers/{data.uuid}/resources";
        }
    }
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        var data = (Models.ListAPIModel.Attributes)e.Parameter;
        if (data is not null)
        {
            NameBox.Text = $"{data.name} ";
            AfterData(data);
        }
        base.OnNavigatedTo(e);
    }
}
