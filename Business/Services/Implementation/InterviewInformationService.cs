using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services.Implementation
{
    internal class InterviewInformationService : IInterviewInformationService
    {
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;
        private readonly Repo.Services.IInterviewService repoInterviewService;

        public InterviewInformationService(DataLoader.Maketalents.Services.IMaketalentsService maketalentsService,
            Repo.Services.IInterviewService repoInterviewService)
        {
            this.maketalentsService = maketalentsService;
            this.repoInterviewService = repoInterviewService;
        }
        public async Task LoadIntervievInformationAsync(int year, CancellationToken cancellationToken)
        {
            var information = await this.maketalentsService.LoadIntervievInformationAsync(year, cancellationToken);
            var insertedVal = new List<Repo.Models.Interview>();

            foreach (var interview in information)
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
    }
}
