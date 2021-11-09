using DataLoader.MyTeam.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.MyTeam.Services.Implementation
{
    internal class MessageService : IMessageService
    {
        private readonly IHttpClientFactory clientFactory;

        public MessageService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task SendMessage(string chatId, string text, CancellationToken cancellationToken)
        {
            var requestSTR = BotInfo.ApiUrl + "/messages/sendText";
            var parametersToAdd = new Dictionary<string, string> {
                { "token", BotInfo.Token },
                { "chatId", chatId },
                { "text", text }
            };
            requestSTR = QueryHelpers.AddQueryString(requestSTR, parametersToAdd);

            var request = new HttpRequestMessage(HttpMethod.Get, requestSTR);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<MessageResponse>(responseStream);
        }
    }
}
