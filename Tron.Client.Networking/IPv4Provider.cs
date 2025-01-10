using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Tron.Client.Networking
{
    internal class IPv4Provider
    {
        public static IPAddress? GetActiveIPv4Address()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up &&
                    networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    !networkInterface.Description.Contains("Virtual"))
                {
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                    UnicastIPAddressInformationCollection unicastAddresses = ipProperties.UnicastAddresses;

                    foreach (UnicastIPAddressInformation unicastAddress in unicastAddresses)
                    {
                        if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return unicastAddress.Address;
                        }
                    }
                }
            }

            return null;
        }
    }
}
