using DataLoader.Maketalents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.Maketalents.Services.Implementation
{
    internal class MaketalentsService : IMaketalentsService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IAuthService authService;
        private readonly Repo.Services.IInterviewService repoInterviewService;
        private readonly Repo.Services.IStaffService repoStaffService;
        private string authToken = "";

        public MaketalentsService(IHttpClientFactory clientFactory, 
            IAuthService authService,
            Repo.Services.IInterviewService repoInterviewService,
            Repo.Services.IStaffService repoStaffService)
        {
            this.clientFactory = clientFactory;
            this.authService = authService;
            this.repoInterviewService = repoInterviewService;
            this.repoStaffService = repoStaffService;
        }

        private async Task<HttpRequestMessage> GetHttpRequestMessageAsync(string requestStr)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestStr);
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
            var requestSTR = "https://smartstaff.simbirsoft1.com/rest/employee/staff";
            var request = await this.GetHttpRequestMessageAsync(requestSTR);

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<IList<SourceStaff>>(responseStream);


            var positions = requestResult.Select(el => el.positions.First()).ToList().Distinct();

            var resultList = requestResult.Where(x => x.city == "Краснодар").ToList();


            foreach (var sourceStaff in resultList)
            {
                var dtoStaff = new Repo.Models.Staff
                {
                    Id = sourceStaff.id,
                    Birthday = DateTime.Parse(sourceStaff.birthday),
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

                await this.repoStaffService.InsertAsync(dtoStaff, cancellationToken);
            }
        }

        public async Task UpdateFiredStaffAsync(int year, CancellationToken cancellationToken)
        {
            var requestSTR = $"https://smartstaff.simbirsoft1.com/rest/chart/firedstaff/{year}";
            var request = await this.GetHttpRequestMessageAsync(requestSTR);
            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var requestResult = await JsonSerializer.DeserializeAsync<IList<FiredStaffResponse>>(responseStream);

            foreach (var st in requestResult)
            {
                var staff = await this.repoStaffService.GetByIdAsync(st.id, cancellationToken);
                if (staff != null && staff.IsActive)
                {
                    staff.IsActive = false;
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                    staff.NotActiveDate = epoch.AddMilliseconds(st.date);
                    await this.repoStaffService.UpdateAsync(staff, cancellationToken);
                }
            }
        }
    }
}
