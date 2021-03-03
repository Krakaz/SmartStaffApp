using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartstaffApp.Models;
using SmartstaffApp.Services;

namespace SmartstaffApp.Pages
{
    /// <summary>
    /// Текущие данные по сотрудникам
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Текущее количество сотрудников + планы
        /// </summary>
        public CurrentDataViewModel CurrentData { get; set; }

        public IList<InformationByMonth> InformationByMonth { get; set; }


        private readonly ILogger<IndexModel> _logger;
        private readonly IStaffService staffService;
        private readonly DataLoader.Maketalents.Services.IMaketalentsService maketalentsService;

        public IndexModel(ILogger<IndexModel> logger, IStaffService staffService, DataLoader.Maketalents.Services.IMaketalentsService maketalentsService)
        {
            _logger = logger;
            this.staffService = staffService;
            this.maketalentsService = maketalentsService;
        }


        public async Task OnGet(CancellationToken cancellationToken)
        {
            var year = DateTime.Now.Year;
            this.CurrentData = await this.staffService.GetCurrentDataAsync(cancellationToken);
            this.InformationByMonth = await this.staffService.GetInformationByMonthAsync(year, cancellationToken);
        }

        private async Task UpdateHeading(MouseEventArgs e)
        {
            this.CurrentData = await this.staffService.GetCurrentDataAsync(CancellationToken.None);
            
        }
        public async Task OnPostUpdateInterview(CancellationToken cancellationToken)
        {
            var year = DateTime.Now.Year;
            await this.maketalentsService.LoadIntervievInformationAsync(year, cancellationToken);
            this.CurrentData = await this.staffService.GetCurrentDataAsync(cancellationToken);
            this.InformationByMonth = await this.staffService.GetInformationByMonthAsync(year, cancellationToken);
        }
        public async Task OnPostUpdateStaff(CancellationToken cancellationToken)
        {
            var year = DateTime.Now.Year;
            await this.maketalentsService.LoadNewStaffAsync(cancellationToken);            
            await this.maketalentsService.UpdateFiredStaffAsync(year, cancellationToken);
            this.CurrentData = await this.staffService.GetCurrentDataAsync(cancellationToken);
            this.InformationByMonth = await this.staffService.GetInformationByMonthAsync(year, cancellationToken);
        }
    }
}