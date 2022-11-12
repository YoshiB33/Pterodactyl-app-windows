using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Pterodactyl_app.core.Helpers;
using Pterodactyl_app.Models.AuthenticateWebsocket;
using Pterodactyl_app.Services;

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
    public async void AfterData(Models.ListAPIModel.Attributes data, TextBlock LogBox, TextBlock StatusBox, TextBlock NameBox)
    {
        string convertBuffer;
        try
        {
            var apiToWebsocket = await APIService.Client.GetAsync($"https://{ServerURL()}/api/client/servers/{data.uuid}/websocket");
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
                            if (FormatedMessage is not null)
                            {
                                switch (FormatedMessage.@event)
                                {
                                    case "status":
                                        if (FormatedMessage.args.Contains("offline"))
                                        {
                                            StatusBox.Text = "Offline";
                                            StatusBox.Foreground = new SolidColorBrush(Colors.Red);
                                        }
                                        else if (FormatedMessage.args.Contains("running"))
                                        {
                                            StatusBox.Text = "Online";
                                            StatusBox.Foreground = new SolidColorBrush(Colors.Green);
                                        }
                                        else if (FormatedMessage.args.Contains("starting"))
                                        {
                                            StatusBox.Text = "Starting";
                                            StatusBox.Foreground = new SolidColorBrush(Colors.Yellow);
                                        }
                                        else if (FormatedMessage.args.Contains("stopping"))
                                        {
                                            StatusBox.Text = "Stopping";
                                            StatusBox.Foreground = new SolidColorBrush(Colors.Orange);
                                        }
                                        break;
                                    case "console output":
                                        foreach (var item in FormatedMessage.args)
                                        {
                                            if (item.EndsWith("[m"))
                                            {
                                                LogBox.Text += $"{item[..^2]}\n";
                                            }
                                            else if (item.EndsWith("[38;2;255;255;255m"))
                                            {
                                                LogBox.Text += $"{item[..^18]}\n";
                                            }
                                            else
                                            {
                                                LogBox.Text += $"{item}\n";
                                            }
                                        }
                                        break;
                                    case "token expiring":
                                        var newApiToWebsocket = await APIService.Client.GetAsync($"https://{ServerURL()}/api/client/servers/{data.uuid}/websocket");
                                        if (apiToWebsocket.IsSuccessStatusCode)
                                        {
                                            LogBox.Text += "Token expiring!\n";
                                            var newWebsocketCredentials = await JsonSerializer.DeserializeAsync<Models.EstablishWebsocket.Rootobject>(newApiToWebsocket.Content.ReadAsStream());
                                            if (newWebsocketCredentials is not null)
                                            {
                                                var newSendToWebsocket = new Rootobject
                                                {
                                                    Event = "auth",
                                                    Args = new string[] { newWebsocketCredentials.data.token },
                                                };
                                                var newAuthJson = JsonSerializer.Serialize(newSendToWebsocket);
                                                await Websocket.Send(Socket, newAuthJson);
                                                LogBox.Text += "Authenticated successfully!\n";
                                            }
                                        }
                                        break;
                                    case "token expired":
                                        LogBox.Text += "Token expired!\n";
                                        AfterData(data, LogBox, StatusBox, NameBox);
                                        break;
                                    case "auth success":
                                        LogBox.Text += "Successfully connected!\n";
                                        break;
                                    case "stats":
                                        break;
                                    case "install output":
                                        foreach (var item in FormatedMessage.args)
                                        {
                                            if (item.EndsWith("[m"))
                                            {
                                                LogBox.Text += $"{item[..^2]}\n";
                                            }
                                            else
                                            {
                                                LogBox.Text += $"{item}\n";
                                            }
                                        }
                                        break;
                                    default:
                                        LogBox.Text += $"No match {FormatedMessage.@event}\n";
                                        break;
                                }
                            }
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
            NameBox.Text += $"{ex.Message} {ServerURL()}/api/client/servers/{data.uuid}/websocket";
        }
    }
}
