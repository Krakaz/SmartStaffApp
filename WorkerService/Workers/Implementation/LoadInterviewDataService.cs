﻿using Microsoft.Extensions.Logging;
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
                var year = DateTime.Now.Year;
                await this.maketalentsService.LoadIntervievInformationAsync(year, cancellationToken);

                await Task.Delay(86400000, cancellationToken);
            }
        }
    }
}