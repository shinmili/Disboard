﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Disboard.Extensions;
using Disboard.Mastodon.Models;

namespace Disboard.Mastodon.Clients
{
    public class FavoritesClient : ApiClient<MastodonClient>
    {
        internal FavoritesClient(MastodonClient client) : base(client, "/api/v1/favourites") { }

        public async Task<List<Status>> ListAsync(long? limit = null, long? sinceId = null, long? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            parameters.AddIfValidValue("limit", limit);
            parameters.AddIfValidValue("since_id", sinceId);
            parameters.AddIfValidValue("max_id", maxId);

            return await GetAsync<List<Status>>(parameters: parameters);
        }
    }
}