using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService.Workers.Implementation
{
    internal class LoadInterviewDataService : ILoadInterviewDataService
    {
        private readonly ILogger<LoadInterviewDataService> logger;
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;

        public LoadInterviewDataService(ILogger<LoadInterviewDataService> logger,
            DataLoader.Maketalents.Services.IMaketalentsService maketalentsService)
        {
            this.logger = logger;
            this.maketalentsService = maketalentsService;
        }
        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await this.maketalentsService.LoadNewStaffAsync(cancellationToken);
                var year = DateTime.Now.Year;
                await this.maketalentsService.UpdateFiredStaffAsync(year, cancellationToken);

                await Task.Delay(28800000, cancellationToken);
            }
        }
    }
}
