﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Disboard.Extensions;
using Disboard.Misskey.Models;

using Newtonsoft.Json.Linq;

namespace Disboard.Misskey.Clients.Drive
{
    public partial class FilesClient : MisskeyApiClient
    {
        protected internal FilesClient(MisskeyClient client) : base(client, "drive/files") { }

        public async Task<List<Note>> AttachedNotesAsync(string fileId)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("fileId", fileId)};

            return await PostAsync<List<Note>>("/attached_notes", parameters).Stay();
        }

        public async Task<File> CheckExistenceAsync(string md5)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("md5", md5)};

            var response = await PostAsync<JObject>("/check_existence", parameters).Stay();
            return response.ContainsKey("file") ? response["file"].ToObject<File>() : null;
        }

        public async Task<File> CreateAsync(string file, string folderId = null, bool? isSensitive = null)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("file", file)};
            parameters.AddIfValidValue("folderId", folderId);
            parameters.AddIfValidValue("isSensitive", isSensitive); // Not work?

            return await PostAsync<File>("/create", parameters).Stay();
        }

        public async Task DeleteAsync(string fileId)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("fileId", fileId)};

            await PostAsync("/delete", parameters).Stay();
        }

        public async Task<FileExtend> ShowAsync(string fileId)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("fileId", fileId)};

            return await PostAsync<FileExtend>("/show", parameters).Stay();
        }

        public async Task<File> UpdateAsync(string fileId, string folderId = null, string name = null, bool? isSensitive = null)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("fileId", fileId)};
            parameters.AddIfValidValue("folderId", folderId);
            parameters.AddIfValidValue("name", name);
            parameters.AddIfValidValue("isSensitive", isSensitive);

            return await PostAsync<File>("/update", parameters).Stay();
        }

        public async Task<File> UploadFromUrlAsync(string url)
        {
            var parameters = new List<KeyValuePair<string, object>> {new KeyValuePair<string, object>("url", url)};

            return await PostAsync<File>("/upload_from_url", parameters).Stay();
        }
    }
}