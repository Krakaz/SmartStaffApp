using DataLoader.Maketalents.Models;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly Repo.Services.IInterviewService repoInterviewService;
        private readonly Repo.Services.IStaffService repoStaffService;
        private readonly MyTeam.Services.IMessageService messageService;
        private string authToken = "";

        public MaketalentsService(ILogger<MaketalentsService> logger, 
            IHttpClientFactory clientFactory, 
            IAuthService authService,
            Repo.Services.IInterviewService repoInterviewService,
            Repo.Services.IStaffService repoStaffService,
            DataLoader.MyTeam.Services.IMessageService messageService)
        {
            this.logger = logger;
            this.clientFactory = clientFactory;
            this.authService = authService;
            this.repoInterviewService = repoInterviewService;
            this.repoStaffService = repoStaffService;
            this.messageService = messageService;
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

        public async Task LoadIntervievInformationAsync(int year, CancellationToken cancellationToken)
        {
            var requestSTR = $"https://smartstaff.simbirsoft1.com/rest/chart/interviewyeardata/{year}?splitByVacancies=true&city=2694145";
            var request = await this.GetHttpRequestMessageAsync(requestSTR);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<IList<InterviewModel>>(responseStream);


            var insertedVal = new List<Repo.Models.Interview>();

            foreach (var interview in requestResult)
            {
                foreach (var month in interview.values)
                {
                    var record = new Repo.Models.Interview()
                    {
                        Year = year,
                        Month = (Repo.Models.Month)month.key + 1,
                        InterviewCount = month.value,
                        PositionName = interview.label
                    };
                    insertedVal.Add(record);
                }
            }

            await this.repoInterviewService.UpsertAsync(insertedVal, year, cancellationToken);            
        }

        public async Task LoadNewStaffAsync(CancellationToken cancellationToken)
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


                foreach (var sourceStaff in resultList)
                {

                    var dtoStaff = new Repo.Models.Staff
                    {
                        Id = sourceStaff.id,
                        Birthday = string.IsNullOrEmpty(sourceStaff.birthday) ? (DateTime?)null : DateTime.Parse(sourceStaff.birthday),
                        Email = sourceStaff.email,
                        Female = sourceStaff.female,
                        FirstName = sourceStaff.firstName,
                        LastName = sourceStaff.lastName,
                        MiddleName = sourceStaff.middleName,
                        FullName = sourceStaff.fullName,
                        FirstWorkingDate = DateTime.Parse(sourceStaff.firstWorkingDate),
                        Phones = string.Join(", ", sourceStaff.phones),
                        Skype = sourceStaff.skype,
                        IsArived = false,
                        IsActive = true,
                        Groups = new List<Repo.Models.Group>(),
                        Positions = new List<Repo.Models.Position>()
                    };

                    foreach (var sourceGroup in sourceStaff.groups)
                    {
                        dtoStaff.Groups.Add(
                            new Repo.Models.Group() { Id = sourceGroup.id, Name = sourceGroup.name }
                            );
                    }

                    foreach (var sourcePosition in sourceStaff.positions)
                    {
                        dtoStaff.Positions.Add(
                            new Repo.Models.Position() { Id = sourcePosition.id, Name = sourcePosition.name }
                            );
                    }

                    dtoStaff.City = new Repo.Models.City { Name = sourceStaff.city };

                    var staff = await this.repoStaffService.GetByIdAsync(sourceStaff.id, cancellationToken);
                    if(staff is null)
                    {
                        await this.messageService.SendMessageToLeadersAsync(1, $"{dtoStaff.FullName} новый сотрудник. На позиции {string.Join(", ", dtoStaff.Positions.Select(el => el.Name))}", cancellationToken);
                    }

                    await this.repoStaffService.InsertAsync(dtoStaff, cancellationToken);                    
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            logger.LogInformation("MaketalentsService Staff Data Loaded");
        }

        public async Task UpdateFiredStaffAsync(int year, CancellationToken cancellationToken)
        {
            var requestSTR = $"https://smartstaff.simbirsoft1.com/rest/chart/firedstaff/{year}";
            var request = await this.GetHttpRequestMessageAsync(requestSTR);
            var client = clientFactory.CreateClient();

            logger.LogInformation("MaketalentsService Start FiredStaff Data Request");
            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<IList<FiredStaffResponse>>(responseStream);

            logger.LogInformation("MaketalentsService FiredStaff Data Geted");
            foreach (var st in requestResult)
            {
                var staff = await this.repoStaffService.GetByIdAsync(st.id, cancellationToken);
                if (staff != null && staff.IsActive)
                {
                    staff.IsActive = false;
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                    staff.NotActiveDate = epoch.AddMilliseconds(st.date);
                    await this.repoStaffService.UpdateAsync(staff, cancellationToken);
                    await this.messageService.SendMessageToLeadersAsync(2, $"{staff.FullName} уволен с позиции {staff.Positions.First().Name}.", cancellationToken);

                }
            }

            logger.LogInformation("MaketalentsService Start FiredStaff Data Loaded");
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
