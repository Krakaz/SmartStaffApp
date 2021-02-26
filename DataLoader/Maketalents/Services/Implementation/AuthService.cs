
using DataLoader.Maketalents.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataLoader.Maketalents.Services.Implementation
{
    internal class AuthService : IAuthService
    {
        private readonly string userName = "denis.kuznetcov";
        private readonly string userPassword = "Pfhfneinhf238";
        private readonly IHttpClientFactory clientFactory;

        public AuthService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<string> GetToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://smartstaff.simbirsoft1.com/rest/identity");
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Host", "smartstaff.simbirsoft1.com");
            request.Headers.Add("Cache-Control", "no-cache");

            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + userPassword));
            request.Headers.Add("Authorization", "Basic " + encoded);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var resultToken = await JsonSerializer.DeserializeAsync<AuthResposeModel>(responseStream);
            return resultToken.authctoken;
        }
    }
}
