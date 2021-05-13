using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DataLoader.Pozdravlala.Models;
using Newtonsoft.Json;

namespace DataLoader.Pozdravlala.Services.Implementation
{
    internal class PozdravlalaService : IPozdravlalaService
    {
        private readonly IHttpClientFactory clientFactory;

        public PozdravlalaService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<string> GetCongratulationAsync(CongratulationRequest requestData, CancellationToken cancellationToken)
        {
            var url = "https://pozdravlala.ru/gen";
            var contentList = new List<int> { (int)requestData.Type, (int)requestData.Sex, (int)requestData.Length, (int)requestData.Appeal, 0 };
            var content = new StringContent(JsonConvert.SerializeObject(contentList), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            var responseStr = await response.Content.ReadAsStringAsync();
            var requestResult = await System.Text.Json.JsonSerializer.DeserializeAsync<CongratulationResponse>(responseStream);

            return requestResult.text;
        }
    }
}
