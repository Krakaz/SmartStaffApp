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
        public async Task<ChatMembers> GetChatMembersAsync(string chatId, CancellationToken cancellationToken)
        {
            var requestSTR = BotInfo.ApiUrl + "/chats/getMembers";
            var parametersToAdd = new Dictionary<string, string> {
                { "token", BotInfo.Token },
                { "chatId", chatId },
            };
            requestSTR = QueryHelpers.AddQueryString(requestSTR, parametersToAdd);

            var request = new HttpRequestMessage(HttpMethod.Get, requestSTR);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<ChatMembers>(responseStream);

            return requestResult;
        }

        public Task<ChatMembers> GetMainChanalMembersAsync(CancellationToken cancellationToken)
        {
            return this.GetChatMembersAsync(BotInfo.MainChanalId, cancellationToken);
        }

        public Task<ChatMembers> GetMainChatMembersAsync(CancellationToken cancellationToken)
        {
            return this.GetChatMembersAsync(BotInfo.MainChatId, cancellationToken);
        }
    }
}
