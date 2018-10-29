﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Disboard.Clients;
using Disboard.Extensions;
using Disboard.Misskey.Models;

namespace Disboard.Misskey.Clients.Messaging
{
    public class MessagesClient : ApiClient<MisskeyClient>
    {
        protected internal MessagesClient(MisskeyClient client) : base(client, "/api/messaging/messages") { }

        public async Task<Message> CreateAsync(string userId, string text = null, string fileId = null)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("userId", userId)};
            parameters.AddIfValidValue("text", text);
            parameters.AddIfValidValue("fileId", fileId);

            return await PostAsync<Message>("/create", parameters).Stay();
        }

        public async Task ReadAsync(string messageId)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("messageId", messageId)};

            await PostAsync("/read", parameters).Stay();
        }
    }
}