using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ONIT_Zad2
{
    public class ConnectionsVM : BaseVM
    {
        public struct ConnectionData
        {
            public string LocalAddress { get; set; }
            public string RemoteAddress { get; set; }
            public string State { get; set; }
        }



        public IEnumerable<ConnectionData> Connections { get; private set; }

        public ConnectionsVM()
        {
            Task.Run(() =>
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

                Connections = tcpConnections.Select(c => new ConnectionData
                {
                    LocalAddress = c.LocalEndPoint.ToString(),
                    RemoteAddress = c.RemoteEndPoint.ToString(),
                    State = c.State.ToString()
                });
            }).ConfigureAwait(false);
        }
    }
}
