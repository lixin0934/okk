using System.Net;
using System.Net.Sockets;

const int ListenPort = 8888;
const string ForwardHost = "127.0.0.1";
const int ForwardPort = 9999;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

var listener = new TcpListener(IPAddress.Any, ListenPort);
listener.Start();
Console.WriteLine($"[启动] 正在监听 0.0.0.0:{ListenPort}");
Console.WriteLine($"[转发] 目标地址 {ForwardHost}:{ForwardPort}");
Console.WriteLine("按 Ctrl+C 停止。\n");

try
{
    while (!cts.Token.IsCancellationRequested)
    {
        var incomingClient = await listener.AcceptTcpClientAsync(cts.Token);
        _ = Task.Run(() => HandleClientAsync(incomingClient, cts.Token), cts.Token);
    }
}
catch (OperationCanceledException)
{
    // graceful shutdown
}
finally
{
    listener.Stop();
    Console.WriteLine("[停止] 监听已关闭。");
}

static async Task HandleClientAsync(TcpClient incomingClient, CancellationToken cancellationToken)
{
    var remoteEndPoint = incomingClient.Client.RemoteEndPoint?.ToString() ?? "unknown";
    Console.WriteLine($"[连接] 来自 {remoteEndPoint}");

    using (incomingClient)
    {
        try
        {
            using var outgoingClient = new TcpClient();
            await outgoingClient.ConnectAsync(ForwardHost, ForwardPort, cancellationToken);
            Console.WriteLine($"[连接] 已连接转发目标 {ForwardHost}:{ForwardPort}");

            using var incomingStream = incomingClient.GetStream();
            using var outgoingStream = outgoingClient.GetStream();

            var buffer = new byte[8192];
            while (!cancellationToken.IsCancellationRequested)
            {
                var bytesRead = await incomingStream.ReadAsync(buffer, cancellationToken);
                if (bytesRead == 0)
                {
                    break;
                }

                await outgoingStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                await outgoingStream.FlushAsync(cancellationToken);
                Console.WriteLine($"[转发] 来自 {remoteEndPoint} 的 {bytesRead} 字节已发送到 {ForwardHost}:{ForwardPort}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[错误] 处理 {remoteEndPoint} 时失败: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"[断开] {remoteEndPoint}");
        }
    }
}
