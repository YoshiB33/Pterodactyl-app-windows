using System.Net.WebSockets;
using System.Text;

namespace Pterodactyl_app.core.Helpers;
public sealed class Websocket
{
    public static async Task Send(ClientWebSocket socket, string data) =>
    await socket.SendAsync(Encoding.UTF8.GetBytes(data), WebSocketMessageType.Text, true, CancellationToken.None);
}
