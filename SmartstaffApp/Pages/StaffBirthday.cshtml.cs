using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartstaffApp.Services;

namespace SmartstaffApp.Pages
{
    public class StaffBirthday : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IStaffService staffService;

        public IList<BirthdayStaffVM> BirthdayStaffs { get; set; }

        public StaffBirthday(ILogger<IndexModel> logger,
            IStaffService staffService)
        {
            this.logger = logger;
            this.staffService = staffService;
        }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var staff = await this.staffService.GetStaffAsync(cancellationToken);

            this.BirthdayStaffs = staff.Where(el => el.IsActive)
                .Where(el => el.Birthday?.Month == DateTime.Now.Month)
                .Select(el => new BirthdayStaffVM
                {
                    Id = el.Id,
                    Birthday = el.Birthday.Value,
                    FullName = el.FullName,
                    Day = el.Birthday.Value.Day,
                    Direction = el.Direction
                })
                .OrderBy(el => el.Day)
                .ToList();
        }
    }
}