using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public List<SelectListItem> SignificantStatus { get; set; } = new List<SelectListItem>()
            {
                new SelectListItem { Value = "0", Text = "Все" },
                new SelectListItem { Value = "1", Text = "Значимые" },
            };

        public InterviewFilter Filter { get; set; } = new InterviewFilter();

        public InterviewModel(ILogger<IndexModel> logger, IStaffService staffService)
        {
            this.logger = logger;
            this.staffService = staffService;
        }
        public async Task OnGet(InterviewFilter filter, CancellationToken cancellationToken)
        {
            var year = DateTime.Now.Year;
            var result = await this.staffService.GetDetailInformationByMonthAsync(Convert.ToBoolean(filter.SignificantStatusId), year, cancellationToken);
            this.DetailInformationByMonth = result;
        }
    }

    public class InterviewFilter
    {
        public int SignificantStatusId { get; set; }
    }
}