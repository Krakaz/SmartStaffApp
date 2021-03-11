using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartstaffApp.Models;
using SmartstaffApp.Services;

namespace SmartstaffApp.Pages
{
    public class InterviewModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IStaffService staffService;

        public IList<DetailInformationByMonth> DetailInformationByMonth { get; set; }

        public InterviewModel(ILogger<IndexModel> logger, IStaffService staffService)
        {
            this.logger = logger;
            this.staffService = staffService;
        }
        public async Task OnGet(int isSignificant, CancellationToken cancellationToken)
        {
            var year = DateTime.Now.Year;
            var result = await this.staffService.GetDetailInformationByMonthAsync(Convert.ToBoolean(isSignificant), year, cancellationToken);
            this.DetailInformationByMonth = result;
        }
    }
}