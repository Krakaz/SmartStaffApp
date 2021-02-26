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
    public class StaffModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IStaffService staffService;

        public List<SelectListItem> Directions { get; set; }

        public IList<StaffVM> Staffs { get; set; }
        public IList<ShortActiveStaffVM> ShortActiveStaffs { get; set; }


        public StaffModel(ILogger<IndexModel> logger,
            IStaffService staffService)
        {
            this.logger = logger;
            this.staffService = staffService;
        }
        public async Task OnGet(StaffFilter filter, CancellationToken cancellationToken)
        {
            await this.FillDirectionsAsync(cancellationToken);
            await this.FillFilteredStaffs(filter, cancellationToken);

            this.Staffs.GroupBy(el => el.Direction).Select(cl => new 
            {
                Direction = cl.First().Direction,
                Count = cl.Count()
            }).ToList();
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

        private async Task FillFilteredStaffs(StaffFilter filter, CancellationToken cancellationToken)
        {
            var staff = await this.staffService.GetStaffAsync(cancellationToken);
            
            this.ShortActiveStaffs = staff.Where(el => el.IsActive).GroupBy(el => el.Direction).Select(cl => new ShortActiveStaffVM
            {
                DirectionId = cl.First().DirectionId,
                DirectionName = cl.First().Direction,
                StaffCount = cl.Count()
            }).ToList();

            if (filter.StaffStatus != 0)
            {                
                staff = staff.Where(el => el.IsActive != Convert.ToBoolean(filter.StaffStatus - 1)).ToList();
            }

            if(filter.StaffArivedStatus != 0)
            {
                staff = staff.Where(el => el.IsArived == Convert.ToBoolean(filter.StaffArivedStatus - 1)).ToList();
            }

            if (filter.DirectionId != 0)
            {
                staff = staff.Where(el => el.DirectionId == filter.DirectionId).ToList();
            }


            this.Staffs = staff.OrderBy(el => el.FullName).ToList();
        }
    }

    /// <summary>
    /// Фильтр по выбору сотрудников
    /// </summary>
    public class StaffFilter
    {
        /// <summary>
        /// Статус сотрудника (0 - Все, 1 - Активные, 2 - Уволенные)
        /// </summary>
        public int StaffStatus { get; set; } = 1;

        /// <summary>
        /// Статус прибытия (0 - все, 1 - Нанятые в Краснодаре, 2 - Понаехи)
        /// </summary>
        public int StaffArivedStatus { get; set; } = 0;

        /// <summary>
        /// Направление сотрудника
        /// </summary>
        public int DirectionId { get; set; }
    }
}