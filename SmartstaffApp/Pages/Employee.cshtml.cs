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
    public class EmployeeModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IEmployeeService employeeService;

        public List<SelectListItem> Directions { get; set; }

        public IList<EmployeeVM> Employees { get; set; }
        public IList<ShortActiveEmployeeVM> ShortActiveEmployees { get; set; }


        public EmployeeModel(ILogger<IndexModel> logger,
            IEmployeeService employeeService)
        {
            this.logger = logger;
            this.employeeService = employeeService;
        }
        public async Task OnGet(EmployeeFilter filter, CancellationToken cancellationToken)
        {
            await this.FillFilteredEmployees(filter, cancellationToken);
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

            if (filter.StaffStatus != 0)
            {
                employees = employees.Where(el => el.IsActive != Convert.ToBoolean(filter.StaffStatus - 1)).ToList();
            }

            if(filter.StaffArivedStatus != 0)
            {
                employees = employees.Where(el => el.IsArived == Convert.ToBoolean(filter.StaffArivedStatus - 1)).ToList();
            }

            this.Employees = employees.OrderBy(el => el.FullName).ToList();
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
        public int StaffStatus { get; set; } = 1;

        /// <summary>
        /// Статус прибытия (0 - все, 1 - Нанятые в Краснодаре, 2 - Понаехи)
        /// </summary>
        public int StaffArivedStatus { get; set; } = 0;
    }
}