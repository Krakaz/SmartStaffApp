using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<SelectListItem> Directions { get; set; }

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
            await this.FillDirectionsAsync(cancellationToken);
            this.DetailInformationByMonth = await this.staffService.GetDetailInformationByMonthAsync(filter, cancellationToken);
        }

        private async Task FillDirectionsAsync(CancellationToken cancellationToken)
        {
            var directions = (await this.staffService.GetPositionsAsync(cancellationToken)).Where(el => el.Childs.Count() != 0).ToList();
            this.Directions = new List<SelectListItem>();
            this.Directions.Add(new SelectListItem { Value = "0", Text = "Все" });
            this.Directions.AddRange(directions.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToList());
        }
    }

    public class InterviewFilter
    {
        /// <summary>
        /// Показывать только значимые
        /// </summary>
        public bool IsSignificant { get; set; }
        /// <summary>
        /// Вывод в сокращенном виде
        /// </summary>
        public bool IsShort { get; set; }

        public int Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// Направление сотрудника
        /// </summary>
        public int DirectionId { get; set; }
    }
}