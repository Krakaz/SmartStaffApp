using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
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
    public class EmployeeModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IEmployeeService employeeService;

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

        public IList<EmployeeVM> Employees { get; set; }
        public IList<ShortActiveEmployeeVM> ShortActiveEmployees { get; set; }

        public EmployeeFilter Filter { get; set; } = new EmployeeFilter();
        public EmployeeSort Sort { get; set; } = new EmployeeSort();


        public EmployeeModel(ILogger<IndexModel> logger,
            IEmployeeService employeeService)
        {
            this.logger = logger;
            this.employeeService = employeeService;

            Type employeeSortType = typeof(EmployeeSort);
            foreach (var f in employeeSortType.GetProperties())
            {
                employeeSortType.GetProperty(f.Name).SetValue(this.Sort, f.Name.ToLower());
            }
        }
        public async Task OnGet(EmployeeFilter filter, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(HttpContext.Request.QueryString.ToString()))
            {
                var sessionFilter = HttpContext.Session.Get<EmployeeFilter>("EmployeeFilter");
                if (sessionFilter != null)
                {
                    filter = sessionFilter;
                }
            }
            if (string.IsNullOrEmpty(filter.SortOrder) && !string.IsNullOrEmpty(this.Filter.SortOrder))
            {
                filter.SortOrder = this.Filter.SortOrder;
            }

            this.Filter = filter;
            await this.FillFilteredEmployees(filter, cancellationToken);
            this.SortStaffs(this.Filter.SortOrder);
            HttpContext.Session.Set<EmployeeFilter>("EmployeeFilter", filter);
        }

        private async Task FillFilteredEmployees(EmployeeFilter filter, CancellationToken cancellationToken)
        {
            var employees = await this.employeeService.GetAllEmployeesAsync(cancellationToken);
            employees = employees.Where(el => el.DirectionId == 1).ToList();
            
            this.ShortActiveEmployees = employees.Where(el => el.IsActive).GroupBy(el => el.PositionId).Select(cl => new ShortActiveEmployeeVM
            {
                PositionId = cl.First().PositionId,
                PositionName = cl.First().Position,
                StaffCount = cl.Count()
            }).ToList();

            if (filter.StaffStatusId != 0)
            {
                employees = employees.Where(el => el.IsActive != Convert.ToBoolean(filter.StaffStatusId - 1)).ToList();
            }

            if(filter.StaffArivedStatusId != 0)
            {
                employees = employees.Where(el => el.IsArived == Convert.ToBoolean(filter.StaffArivedStatusId - 1)).ToList();
            }

            this.Employees = employees.OrderBy(el => el.FullName).ToList();
        }

        private void SortStaffs(string sortOrder)
        {
            Type employeeSortType = typeof(EmployeeSort);
            var isDesc = false;
            var descStr = "_desc";

            if (!string.IsNullOrEmpty(sortOrder) && sortOrder.IndexOf(descStr) != -1)
            {
                isDesc = true;
                sortOrder = sortOrder.Substring(0, sortOrder.IndexOf(descStr));
            }

            if(!string.IsNullOrEmpty(sortOrder) && employeeSortType.GetProperties().Any(x => x.Name.ToLower() == sortOrder))
            {
                var field = employeeSortType.GetProperties().First(x => x.Name.ToLower() == sortOrder);
                var sortedField = typeof(EmployeeVM).GetField(field.Name, BindingFlags.Public | BindingFlags.Instance);
                if(!isDesc)
                {
                    this.Employees = this.Employees.OrderBy(el => el.GetType().GetProperty(field.Name).GetValue(el, null)).ToList();
                    employeeSortType.GetProperty(field.Name).SetValue(this.Sort, field.Name.ToLower() + descStr);
                }
                else
                {
                    this.Employees = this.Employees.OrderByDescending(el => el.GetType().GetProperty(field.Name).GetValue(el, null)).ToList();
                    employeeSortType.GetProperty(field.Name).SetValue(this.Sort, field.Name.ToLower());
                }                            
            }
        }
    }

    /// <summary>
    /// Фильтр по выбору сотрудников
    /// </summary>
    public class EmployeeFilter
    {
        /// <summary>
        /// Статус сотрудника (0 - Все, 1 - Активные, 2 - Уволенные)
        /// </summary>
        public int StaffStatusId { get; set; } = 1;

        /// <summary>
        /// Статус прибытия (0 - все, 1 - Нанятые в Краснодаре, 2 - Понаехи)
        /// </summary>
        public int StaffArivedStatusId { get; set; } = 0;

        public string SortOrder { get; set; }
    }

    /// <summary>
    /// Сортировка списка сотрудников
    /// </summary>
    public class EmployeeSort
    {
        public string FullName { get; set; }
        public string FirstWorkingDate { get; set; }
        public string Position { get; set; }
        public string Phones { get; set; }
        public string Quality { get; set; }
        public string Values { get; set; }
        public string Salary { get; set; }
        public string RevisionDate { get; set; }
    }
}