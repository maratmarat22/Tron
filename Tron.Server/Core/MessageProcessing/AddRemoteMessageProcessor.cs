﻿using System.Net;
using Tron.Common.Messages;
using Tron.Common.Networking;
using Tron.Server.Networking;

namespace Tron.Server.Core.MessageProcessing
{
    internal class AddRemoteMessageProcessor : IMessageProcessor
    {
        public Message Process(Message message, Dictionary<string, string?> state, ICaster caster)
        {
            bool added = ((Multicaster)caster).AddRemote(IPEndPoint.Parse(message.Payload[0]));

            return new Message(Header.Acknowledge, [message.Header.ToString()]);
        }
    }
}
