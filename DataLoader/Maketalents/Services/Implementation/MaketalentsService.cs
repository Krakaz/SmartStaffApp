using DataLoader.Maketalents.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.Maketalents.Services.Implementation
{
    internal class MaketalentsService : IMaketalentsService
    {
        private readonly ILogger<MaketalentsService> logger;
        private readonly IHttpClientFactory clientFactory;
        private readonly IAuthService authService;
        private string authToken = "";

        public MaketalentsService(ILogger<MaketalentsService> logger, 
            IHttpClientFactory clientFactory, 
            IAuthService authService)
        {
            this.logger = logger;
            this.clientFactory = clientFactory;
            this.authService = authService;
        }

        private async Task<HttpRequestMessage> GetHttpRequestMessageAsync(string requestStr) => await AddHttpRequestMessageHeader(new HttpRequestMessage(HttpMethod.Get, requestStr));

        private async Task<HttpRequestMessage> AddHttpRequestMessageHeader(HttpRequestMessage request)
        {
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Host", "smartstaff.simbirsoft1.com");
            request.Headers.Add("Cache-Control", "no-cache");

            if (authToken == "")
            {
                authToken = await this.authService.GetToken();
            }

            request.Headers.Add("Authorization", "Bearer " + authToken);

            return request;
        }

        public async Task<IList<InterviewModel>> LoadIntervievInformationAsync(int year, CancellationToken cancellationToken)
        {
            var requestSTR = $"https://smartstaff.simbirsoft1.com/rest/chart/interviewyeardata/{year}?splitByVacancies=true&city=2694145";
            var request = await this.GetHttpRequestMessageAsync(requestSTR);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IList<InterviewModel>>(responseStream);         
        }

        public async Task<IList<SourceStaff>> LoadNewStaffAsync(CancellationToken cancellationToken)
        {
            var requestSTR = "https://smartstaff.simbirsoft1.com/rest/employee/staff";//?ignoreRestrictions=true";
            var request = await this.GetHttpRequestMessageAsync(requestSTR);

            var client = clientFactory.CreateClient();


            logger.LogInformation("MaketalentsService Start Staff Data Request");
            var response = await client.SendAsync(request);
            
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<IList<SourceStaff>>(responseStream);
            logger.LogInformation("MaketalentsService Staff Data Gated");

            try
            {            
                //var resultList2 = requestResult.Where(x => x.city == "Ростов-на-Дону").ToList();
                //var resultList3 = requestResult.Where(x => x.city == "Таганрог").ToList();

                var resultList = requestResult.Where(x => x.groups.Any(g => g.id == 3784995)).ToList(); //Филиал Краснодар
                return resultList;                
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return new List<SourceStaff>();
            logger.LogInformation("MaketalentsService Staff Data Loaded");
        }
        
        public async Task<ApplicantsResponce> LoadApplicantsAsync(CancellationToken cancellationToken)
        {
            var requestSTR = $"https://smartstaff.simbirsoft1.com/rest/employee/applicantList";
            var parametersToAdd = new ApplicantsRequest
            {
                conjunctionRestrictions = new List<ConjunctionRestriction>
                {
                    new ConjunctionRestriction { field = "city", @operator = "EQUALS", value = "Краснодар" },
                    new ConjunctionRestriction { field = "status", @operator = "LIKE", value = "%оффер" },
                },
                disjunctionRestrictions = new List<object>(),
                sortOrders = new List<SortOrder> { new SortOrder { field = "id", direction = "ASC" } }
            };

            var str = JsonSerializer.Serialize(parametersToAdd);
            var content = new StringContent(JsonSerializer.Serialize(parametersToAdd), Encoding.UTF8, "application/json");
            var request = await this.AddHttpRequestMessageHeader(new HttpRequestMessage(HttpMethod.Post, requestSTR) { Content = content });
            var client = clientFactory.CreateClient();

            logger.LogInformation("MaketalentsService Start Applicants Data Request");
            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<ApplicantsResponce>(responseStream);

            return requestResult;
        }
    }
}
