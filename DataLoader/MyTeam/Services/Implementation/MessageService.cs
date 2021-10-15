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
        private readonly Repo.Services.INotificationEmailsService notificationEmailsService;

        public MessageService(IHttpClientFactory clientFactory, Repo.Services.INotificationEmailsService notificationEmailsService)
        {
            this.clientFactory = clientFactory;
            this.notificationEmailsService = notificationEmailsService;
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

        public async Task SendMessageToLeadersAsync(int messageTypeId, string text, CancellationToken cancellationToken)
        {
            var emails = await this.notificationEmailsService.GetAllAsync(cancellationToken);
            foreach(var email in emails.Where(el => el.NotificationTypes.Any(tp => tp.Id == messageTypeId)))
            {
                await this.SendMessage(email.Email, text, cancellationToken);
            }
        }
    }
}
