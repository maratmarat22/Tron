﻿using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Tron.Common.Messages;

namespace Tron.Common.Networking
{
    public static class SocketExtensions
    {        
        private static void SendTo(this Socket local, Message message, EndPoint remote)
        {
            string jsonData = JsonSerializer.Serialize(message);
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

            local.SendTo(byteData, remote);
        }

        private static Message ReceiveFrom(this Socket local, ref EndPoint remote)
        {
            byte[] byteData = new byte[1024];
            int bytesRead = local.ReceiveFrom(byteData, ref remote);
            string jsonData = Encoding.ASCII.GetString(byteData, 0, bytesRead);
            
            return JsonSerializer.Deserialize<Message>(jsonData)!;
        }

        public static bool TrySendTo(this Socket sender, Message message, EndPoint receiver)
        {
            try
            {
                sender.SendTo(message, receiver);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static Message? TryReceiveFrom(this Socket receiver, ref EndPoint sender)
        {
            Message? message;

            try
            {
                message = receiver.ReceiveFrom(ref sender);
            }
            catch (Exception)
            {
                message = null;
            }
            
            return message;
        }

        public static Message? TryReceiveFrom(this Socket receiver, EndPoint sender)
        {
            return receiver.TryReceiveFrom(ref sender);
        }

        public static bool TryBind(this Socket socket, IPAddress address, int port)
        {
            bool portAvailable = false;

            while (!portAvailable)
            {
                portAvailable = true;

                try
                {
                    IPEndPoint point = new(address, port);
                    socket.Bind(point);
                }
                catch (SocketException)
                {
                    portAvailable = false;
                    ++port;
                }
            }

            return portAvailable;
        }
    }
}
