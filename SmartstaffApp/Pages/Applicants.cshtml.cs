using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartstaffApp.Models;
using SmartstaffApp.Services;

namespace SmartstaffApp.Pages
{
    public class ApplicantModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IApplicantsService applicantsService;

        public IList<Applicant> Applicants { get; set; }

        public ApplicantModel(ILogger<IndexModel> logger,
            IApplicantsService applicantsService)
        {
            this.logger = logger;
            this.applicantsService = applicantsService;
        }

        public async Task OnGet(StaffFilter filter, CancellationToken cancellationToken)
        {
            this.Applicants = await this.applicantsService.GetApplicantsListAsync(cancellationToken);
        }        
    } 
}