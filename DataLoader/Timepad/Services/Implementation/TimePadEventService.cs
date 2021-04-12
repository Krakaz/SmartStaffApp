using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DataLoader.Timepad.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace DataLoader.Timepad.Services.Implementation
{
    internal class TimePadEventService : ITimePadEventService
    {
        private readonly IHttpClientFactory clientFactory;

        public TimePadEventService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<EventList> GetEventListAsync(CancellationToken cancellationToken)
        {
            var url = "https://api.timepad.ru/v1/events";
            var parametersToAdd = new Dictionary<string, string> {
                { "limit", "100" },
                { "sort", "+id" },
                { "organization_ids", "135235" },
                { "access_statuses", "public" },
                { "moderation_statuses", "featured, shown" },
                { "fields", "location" },
            };
            url = QueryHelpers.AddQueryString(url, parametersToAdd);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", "Bearer d27adf53074b7777cf46cd157a796a9efba015e6");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            var responseStr = await response.Content.ReadAsStringAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<EventList>(responseStream);

            return requestResult;
        }       
    }
}
