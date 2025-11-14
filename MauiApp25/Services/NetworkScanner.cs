using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MauiApp25.Services
{
    public class NetworkScanner
    {
        // Scans common ports on the local subnet for open TCP endpoints.
        public async Task<List<MauiApp25.Models.DeviceInfo>> ScanLocalSubnetAsync(int port = 22, int timeoutMs = 200)
        {
            var results = new List<MauiApp25.Models.DeviceInfo>();

            // Find local IPv4 address and subnet
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            var localIp = hostEntry.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            if (localIp == null)
                return results;

            var ipParts = localIp.ToString().Split('.');
            if (ipParts.Length != 4)
                return results;

            var basePrefix = string.Join('.', ipParts.Take(3)) + ".";

            var tasks = new List<Task>();
            var lockObj = new object();

            for (int i = 1; i < 255; i++)
            {
                var ip = basePrefix + i;
                tasks.Add(Task.Run(async () =>
                {
                    using var client = new TcpClient();
                    try
                    {
                        var connectTask = client.ConnectAsync(ip, port);
                        var completed = await Task.WhenAny(connectTask, Task.Delay(timeoutMs));
                        var isOpen = completed == connectTask && client.Connected;

                        var info = new MauiApp25.Models.DeviceInfo { IpAddress = ip, Port = port, IsOpen = isOpen };
                        lock (lockObj) results.Add(info);
                    }
                    catch
                    {
                        var info = new MauiApp25.Models.DeviceInfo { IpAddress = ip, Port = port, IsOpen = false };
                        lock (lockObj) results.Add(info);
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return results.OrderBy(r => r.IpAddress).ToList();
        }
    }
}
