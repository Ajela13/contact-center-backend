using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public static class WebSocketHandler
{
    private static readonly List<WebSocket> agentSockets = new();
    private static readonly List<WebSocket> clientSockets = new();
    private static readonly Random random = new();

    public static async Task HandleWebSocket(string path, WebSocket webSocket)
    {
        var socketList = path == "/ws/agents" ? agentSockets : clientSockets;
        lock (socketList) socketList.Add(webSocket);

        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                await Task.Delay(30000); // Simular actualización cada 30s
                await SendUpdatedData(socketList, path);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en WebSocket: {ex.Message}");
        }
        finally
        {
            lock (socketList) socketList.Remove(webSocket);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Conexión cerrada", CancellationToken.None);
        }
    }

    private static async Task SendUpdatedData(List<WebSocket> sockets, string path)
    {
        var data = path == "/ws/agents" ? GenerateAgentsData() : GenerateClientsData();
        var json = JsonSerializer.Serialize(data);
        var buffer = Encoding.UTF8.GetBytes(json);

    List<WebSocket> socketsCopy;
    lock (sockets)
    {
        socketsCopy = sockets.ToList();
    }

    await Parallel.ForEachAsync(socketsCopy, async (socket, _) =>
    {
        if (socket.State == WebSocketState.Open)
        {
            await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    });
}

    private static List<object> GenerateAgentsData()
    {
        return Enumerable.Range(1, 10).Select(id => new
        {
            id = id,
            name = $"Agent {id}",
            status = GetRandomStatus(),
            waitTime = random.Next(1, 21)
        }).ToList<object>();
    }

    private static List<object> GenerateClientsData()
    {
        return Enumerable.Range(1, 10).Select(id => new
        {
            id = id + 10, // Evitar conflicto con IDs de agentes
            name = $"Client {id}",
            waitTime = random.Next(1, 21)
        }).ToList<object>();
    }

    private static string GetRandomStatus()
    {
        var statuses = new[] { "available", "busy", "paused" };
        return statuses[random.Next(statuses.Length)];
    }
}