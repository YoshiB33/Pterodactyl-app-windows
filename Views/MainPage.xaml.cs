using System.Text.Json;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Pterodactyl_app.Helpers;
using Pterodactyl_app.Models.ListAPIModel;
using Pterodactyl_app.Services;
using Pterodactyl_app.ViewModels;

namespace Pterodactyl_app.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
        GetServers();
        ProgressRing ring = new();
        ContentArea.Children.Add(ring);
        if (Panel.Children != null)
        {
            ring.IsActive = false;
        }
    }

    private async void GetServers()
    {
        try
        {
            var response = await APIService.Client.GetAsync("https://server.yoshib.se/api/client");

            if (response.IsSuccessStatusCode)
            {
                var res = await JsonSerializer.DeserializeAsync<Rootobject>(response.Content.ReadAsStream());
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (res !=null || res.data != null)
                {
                    foreach (var data in res.data)
                    {
                        Button button = new()
                        {
                            Content = data.attributes.name,
                            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                            Width = 800,
                            Height = 100,
                            FontSize = 25,
                            Margin = new Microsoft.UI.Xaml.Thickness(0, 0, 0, 10),
                        };
                        button.Click += (sender, e) =>
                        {
                            try
                            {
                                Frame.Navigate(typeof(ServerPage), data.attributes);
                            }
                            catch (System.NullReferenceException ex)
                            {
                                TextBox textBox = new()
                                {
                                    Text = $"Exeption: {ex.Message}"
                                };
                                Panel.Children.Add(textBox);
                            }
                        };
                        Panel.Children.Add(button);
                    }
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "'<' is an invalid start of a value. Path: $ | LineNumber: 0 | BytePositionInLine: 0.")
            {
                GetServers();
            }
            else
            {
                TextBox textBox = new()
                {
                    Text = $"Exeption: {ex.Message}"
                };
                Panel.Children.Add(textBox);
            }
        }
    }
}
