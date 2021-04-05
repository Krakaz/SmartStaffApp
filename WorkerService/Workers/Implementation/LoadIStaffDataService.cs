using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService.Workers.Implementation
{
    internal class LoadIStaffDataService : ILoadStaffDataService
    {
        private readonly ILogger<LoadIStaffDataService> logger;
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;

        public LoadIStaffDataService(ILogger<LoadIStaffDataService> logger,
            DataLoader.Maketalents.Services.IMaketalentsService maketalentsService)
        {
            this.logger = logger;
            this.maketalentsService = maketalentsService;
        }
        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) 
            {
                logger.LogInformation("LoadIStaffDataService Hosted Service running.");
                await this.maketalentsService.LoadNewStaffAsync(cancellationToken);
                var year = DateTime.Now.Year;
                await this.maketalentsService.UpdateFiredStaffAsync(year, cancellationToken);
                logger.LogInformation("LoadIStaffDataService Hosted Service stoped.");

                await Task.Delay(28800000, cancellationToken);
            }
        }
    }
}
