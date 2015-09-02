using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace MyMoney.Documentation.Requirements.Common
{
    public static class Host
    {
        private const string LocalHostAddress = "http://localhost:";
        private static string baseAddress = "";
        private static CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Starts the host.
        /// </summary>
        /// <typeparam name="TStartup">The startup class.</typeparam>
        /// <param name="port">Enter a specific port or leave null for the host to utilise one that is available.</param>
        /// <param name="getBaseAddress">Action for returning the base address.</param>
        public static void StartHost<TStartup>(int? port = null, Action<string> getBaseAddress = null)
        {
            baseAddress = GetBaseAddress(port);
            if (getBaseAddress != null)
            {
                getBaseAddress(baseAddress);
            }
            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            Task.Factory.StartNew(() => WebApp.Start<TStartup>(baseAddress), cancellationToken);
        }

        /// <summary>
        /// Stops the host.
        /// </summary>
        public static void StopHost()
        {
            cancellationTokenSource.Cancel();
        }

        private static string GetBaseAddress(int? port)
        {
            return string.Format("{0}{1}/", LocalHostAddress, port.HasValue ? port.Value : GetAvailableTcpPort());
        }

        private static int GetAvailableTcpPort()
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            var port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }
    }
}