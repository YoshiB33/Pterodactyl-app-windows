using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Pterodactyl_app.Services;
using Pterodactyl_app.ViewModels;
using Pterodactyl_app.core.Helpers;
using Pterodactyl_app.Models.AuthenticateWebsocket;
using System.Net.Sockets;
using System.Text;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
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
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    private async void AfterData(Models.ListAPIModel.Attributes data)
    {
        string convertBuffer;
        try
        {
            var apiToWebsocket = await APIService.Client.GetAsync($"https://{APIService.GetServerURL()}/api/client/servers/{data.uuid}/websocket");
            if (apiToWebsocket.IsSuccessStatusCode)
            {
                var websocketCredentials = await JsonSerializer.DeserializeAsync<Models.EstablishWebsocket.Rootobject>(apiToWebsocket.Content.ReadAsStream());
                if (websocketCredentials is not null)
                {
                    do
                    {
                        var Socket = new ClientWebSocket();
                        await Socket.ConnectAsync(new Uri(websocketCredentials.data.socket), CancellationToken.None);
                        var Token = websocketCredentials.data.token;
                        var sendToWebSocket = new Rootobject
                        {
                            Event = "auth",
                            Args = new string[] { websocketCredentials.data.token },
                        };
                        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                        var authJson = JsonSerializer.Serialize(sendToWebSocket, jsonOptions);
                        await Websocket.Send(Socket, authJson);
                        var buffer = new ArraySegment<byte>(new byte[2048]);
                        do
                        {
                            WebSocketReceiveResult result;
                            using var ms = new MemoryStream();
                            do
                            {
                                result = await Socket.ReceiveAsync(buffer, CancellationToken.None);
#pragma warning disable CS8604 // Possible null reference argument.
                                ms.Write(buffer.Array, buffer.Offset, result.Count);
#pragma warning restore CS8604 // Possible null reference argument.
                            } while (!result.EndOfMessage);

                            if (result.MessageType == WebSocketMessageType.Close)
                            {
                                break;
                            }

                            ms.Seek(0, SeekOrigin.Begin);
                            using var reader = new StreamReader(ms, Encoding.UTF8);
                            convertBuffer = await reader.ReadToEndAsync();
                            var FormatedMessage = JsonSerializer.Deserialize<Models.WebSocketModel.Rootobject>(convertBuffer);
                            LogBox.Text += convertBuffer;
                        } while (true);
                    } while (true);
                }
            }
            else
            {
                NameBox.Text += apiToWebsocket.ToString();
            }
        }
        catch (Exception ex)
        {
            NameBox.Text += $"{ex.Message} {ViewModel.ServerURL()}/api/client/servers/{data.uuid}/websocket";
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
