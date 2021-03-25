using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SmartstaffApp.Extensions;
using SmartstaffApp.Models;
using SmartstaffApp.Services;

namespace SmartstaffApp.Pages
{
    public class StaffModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IStaffService staffService;

        public List<SelectListItem> Directions { get; set; }
        public List<SelectListItem> StaffStatus { get; set; } = new List<SelectListItem>()
            {
                new SelectListItem { Value = "0", Text = "Все" },
                new SelectListItem { Value = "1", Text = "Активные" },
                new SelectListItem { Value = "2", Text = "Уволенные" },
            };
        public List<SelectListItem> StaffArivedStatus { get; set; } = new List<SelectListItem>()
            {
                new SelectListItem { Value = "0", Text = "Все" },
                new SelectListItem { Value = "1", Text = "Нанятые в Краснодаре" },
                new SelectListItem { Value = "2", Text = "Понаехавшие" },
            };

        public IList<StaffVM> Staffs { get; set; }
        public IList<ShortActiveStaffVM> ShortActiveStaffs { get; set; }
        public StaffFilter Filter { get; set; } = new StaffFilter();
        public StaffSort Sort { get; set; } = new StaffSort();



        public StaffModel(ILogger<IndexModel> logger,
            IStaffService staffService)
        {
            this.logger = logger;
            this.staffService = staffService;

            Type employeeSortType = typeof(StaffSort);
            foreach (var f in employeeSortType.GetProperties())
            {
                employeeSortType.GetProperty(f.Name).SetValue(this.Sort, f.Name.ToLower());
            }
        }

        public async Task OnGet(StaffFilter filter, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(HttpContext.Request.QueryString.ToString()))
            {
                var sessionFilter = HttpContext.Session.Get<StaffFilter>("StaffFilter");
                if (sessionFilter != null)
                {
                    filter = sessionFilter;
                }                
            }

            if(string.IsNullOrEmpty(filter.SortOrder) && !string.IsNullOrEmpty(this.Filter.SortOrder))
            {
                filter.SortOrder = this.Filter.SortOrder;
            }

            this.Filter = filter;

            await this.FillDirectionsAsync(cancellationToken);
            await this.FillFilteredStaffs(this.Filter, cancellationToken);
            this.SortStaffs(this.Filter.SortOrder);
            HttpContext.Session.Set<StaffFilter>("StaffFilter", filter);
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

            if (filter.StaffStatusId != 0)
            {                
                staff = staff.Where(el => el.IsActive != Convert.ToBoolean(filter.StaffStatusId - 1)).ToList();
            }

            if(filter.StaffArivedStatusId != 0)
            {
                staff = staff.Where(el => el.IsArived == Convert.ToBoolean(filter.StaffArivedStatusId - 1)).ToList();
            }

            if (filter.DirectionId != 0)
            {
                staff = staff.Where(el => el.DirectionId == filter.DirectionId).ToList();
            }


            this.Staffs = staff.OrderBy(el => el.FullName).ToList();
        }

        private void SortStaffs(string sortOrder)
        {
            Type employeeSortType = typeof(StaffSort);
            var isDesc = false;
            var descStr = "_desc";

            if (!string.IsNullOrEmpty(sortOrder) && sortOrder.IndexOf(descStr) != -1)
            {
                isDesc = true;
                sortOrder = sortOrder.Substring(0, sortOrder.IndexOf(descStr));
            }

            if (!string.IsNullOrEmpty(sortOrder) && employeeSortType.GetProperties().Any(x => x.Name.ToLower() == sortOrder))
            {
                var field = employeeSortType.GetProperties().First(x => x.Name.ToLower() == sortOrder);
                var sortedField = typeof(EmployeeVM).GetField(field.Name, BindingFlags.Public | BindingFlags.Instance);
                if (!isDesc)
                {
                    this.Staffs = this.Staffs.OrderBy(el => el.GetType().GetProperty(field.Name).GetValue(el, null)).ToList();
                    employeeSortType.GetProperty(field.Name).SetValue(this.Sort, field.Name.ToLower() + descStr);
                }
                else
                {
                    this.Staffs = this.Staffs.OrderByDescending(el => el.GetType().GetProperty(field.Name).GetValue(el, null)).ToList();
                    employeeSortType.GetProperty(field.Name).SetValue(this.Sort, field.Name.ToLower());
                }
            }
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
        public int StaffStatusId { get; set; } = 1;

        /// <summary>
        /// Статус прибытия (0 - все, 1 - Нанятые в Краснодаре, 2 - Понаехи)
        /// </summary>
        public int StaffArivedStatusId { get; set; } = 0;

        /// <summary>
        /// Направление сотрудника
        /// </summary>
        public int DirectionId { get; set; }

        public string SortOrder { get; set; }
    }

    /// <summary>
    /// Сортировка списка сотрудников
    /// </summary>
    public class StaffSort
    {
        public string FullName { get; set; }
        public string Birthday { get; set; }
        public string FirstWorkingDate { get; set; }
        public string Position { get; set; }
        public string Direction { get; set; }
        public string Phones { get; set; }
        public string NotActiveDate { get; set; }

    }
}