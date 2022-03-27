using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ONIT_Zad2
{
    public class EndPointsVM : BaseVM
    {
        public struct EndPointData
        {
            public string IPAddress { get; set; }
            public int Port { get; set; }
            public string State { get; set; }
        }
        public enum PortType
        {
            Tcp,
            Udp
        }


        public IEnumerable<EndPointData> EndPoints { get; private set; }

        public EndPointsVM(PortType type)
        {
            Task.Run(() =>
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] endPoints = type == PortType.Tcp?properties.GetActiveTcpListeners():properties.GetActiveUdpListeners();

                EndPoints = endPoints.Select(ep => new EndPointData { IPAddress = ep.Address.ToString(), Port = ep.Port, State = "Listening" });
            }).ConfigureAwait(false);
        }


    }
}
