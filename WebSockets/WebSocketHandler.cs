using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System;
using System.Linq;

public static class WebSocketHandler
{
    private static readonly List<WebSocket> agentSockets = new();
    private static readonly List<WebSocket> clientSockets = new();

    private static List<object> latestAgentsData = new();
    private static List<object> latestClientsData = new();

    private static readonly Random random = new();

    static WebSocketHandler()
    {
        latestAgentsData = GenerateAgentsData();
        latestClientsData = GenerateClientsData();
        Task.Run(async () => await BroadcastUpdatedData());
        
        // Iniciar la actualización automática cada 5 segundos
        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(5000); // Cada 5 segundos
                latestAgentsData = GenerateAgentsData();
                latestClientsData = GenerateClientsData();
                await BroadcastUpdatedData();
            }
        });
    }

    public static async Task HandleWebSocket(string path, WebSocket webSocket)
    {
        var socketList = path == "/ws/agents" ? agentSockets : clientSockets;
        lock (socketList) socketList.Add(webSocket);

        try
        {
            await SendUpdatedData(socketList, path);

            while (webSocket.State == WebSocketState.Open)
            {
                await Task.Delay(5000); // Enviar datos cada 5 segundos
                await SendUpdatedData(socketList, path);
            }
        }
        finally
        {
            lock (socketList) socketList.Remove(webSocket);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Conexión cerrada", CancellationToken.None);
        }
    }

    private static async Task BroadcastUpdatedData()
    {
        await SendUpdatedData(agentSockets, "/ws/agents");
        await SendUpdatedData(clientSockets, "/ws/clients");
    }

    private static async Task SendUpdatedData(List<WebSocket> sockets, string path)
    {
        var data = path == "/ws/agents" ? latestAgentsData : latestClientsData;
        var json = JsonSerializer.Serialize(data);
        var buffer = Encoding.UTF8.GetBytes(json);

        List<WebSocket> socketsCopy;
        lock (sockets) { socketsCopy = sockets.ToList(); }

        await Parallel.ForEachAsync(socketsCopy, async (socket, _) =>
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        });
    }

    private static string GetRandomStatus()
    {
        var statuses = new[] { "available", "busy", "paused" };
        return statuses[random.Next(statuses.Length)];
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
            id = id + 10,
            name = $"Client {id}",
            waitTime = random.Next(1, 21)
        }).ToList<object>();
    }

public static List<object> GetLatestAgentsData()
{
    lock (latestAgentsData)
    {
        return latestAgentsData.ToList();
    }
}

public static List<object> GetLatestClientsData()
{
    lock (latestClientsData)
    {
        return latestClientsData.ToList();
    }
}

}
