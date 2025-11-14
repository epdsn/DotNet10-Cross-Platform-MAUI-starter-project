namespace DataObjects
{
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public static class ConnectionManager
    {
        // Attempts to open a TCP connection to the specified ip and port with a timeout.
        public static async Task<bool> IsTcpPortOpenAsync(string ip, int port, int timeoutMs = 200)
        {
            using var client = new TcpClient();
            try
            {
                var connectTask = client.ConnectAsync(ip, port);
                var completed = await Task.WhenAny(connectTask, Task.Delay(timeoutMs));
                return completed == connectTask && client.Connected;
            }
            catch
            {
                return false;
            }
        }
    }
}
