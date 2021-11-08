using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DataLoader.MyTeam.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace DataLoader.MyTeam.Services.Implementation
{
    internal class ChatService : IChatService
    {
        private readonly IHttpClientFactory clientFactory;

        public ChatService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        private async Task<ChatMembers> GetChatMembersAsync(string chatId, string cursor, CancellationToken cancellationToken)
        {
            var requestSTR = BotInfo.ApiUrl + "/chats/getMembers";
            var parametersToAdd = new Dictionary<string, string> {
                { "token", BotInfo.Token },
                { "chatId", chatId },
            };

            if(!string.IsNullOrEmpty(cursor))
            {
                parametersToAdd.Add("cursor", cursor);
            }

            requestSTR = QueryHelpers.AddQueryString(requestSTR, parametersToAdd);

            var request = new HttpRequestMessage(HttpMethod.Get, requestSTR);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<ChatMembers>(responseStream);

            return requestResult;
        }

        public async Task<ChatMembers> GetMainChanalMembersAsync(CancellationToken cancellationToken)
        {
            var result = await this.GetChatMembersAsync(BotInfo.MainChanalId, "", cancellationToken);
            var cursor = result.cursor;
            while(!string.IsNullOrEmpty(cursor))
            {
                var secondResult = await this.GetChatMembersAsync(BotInfo.MainChanalId, result.cursor, cancellationToken);
                foreach (var m in secondResult.members)
                {
                    result.members.Add(m);
                }
                cursor = secondResult.cursor;
            }
            return result;
        }

        public async Task<ChatMembers> GetMainChatMembersAsync(CancellationToken cancellationToken)
        {
            var result = await this.GetChatMembersAsync(BotInfo.MainChatId, "", cancellationToken);
            var cursor = result.cursor;
            while (!string.IsNullOrEmpty(cursor))
            {
                var secondResult = await this.GetChatMembersAsync(BotInfo.MainChatId, result.cursor, cancellationToken);
                foreach (var m in secondResult.members)
                {
                    result.members.Add(m);
                }
                cursor = secondResult.cursor;
            }

            return result;
        }
    }
}
