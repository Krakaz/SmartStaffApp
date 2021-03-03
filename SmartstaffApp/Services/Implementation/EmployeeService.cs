using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartstaffApp.Models;

namespace SmartstaffApp.Services.Implementation
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly Repo.Services.IEmployeeService repoEmployeeService;
        private readonly Repo.Services.IPositionService positionService;

        public EmployeeService(Repo.Services.IEmployeeService repoEmployeeService, 
            Repo.Services.IPositionService positionService)
        {
            this.repoEmployeeService = repoEmployeeService;
            this.positionService = positionService;
        }
        public async Task<IList<EmployeeVM>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            var result = new List<EmployeeVM>();
            var staffs = await this.repoEmployeeService.GetAllAsync(cancellationToken);
            var positions = await this.positionService.GetAllAsync(cancellationToken);
            result = staffs.Select(el => new EmployeeVM
            {
                Id = el.Id,
                ArivedDate = el.ArivedDate,
                Birthday = el.Birthday,
                FirstName = el.FirstName,
                LastName = el.LastName,
                MiddleName = el.MiddleName,
                FullName = el.FullName,
                Email = el.Email,
                Skype = el.Skype,
                Phones = el.Phones,
                Female = el.Female,
                FirstWorkingDate = el.FirstWorkingDate,
                IsActive = el.IsActive,
                IsArived = el.IsArived,
                NotActiveDate = el.NotActiveDate,
                Groups = string.Join(", ", el.Groups),
                PositionId = (int)el.Positions.FirstOrDefault()?.Id,
                Position = el.Positions.FirstOrDefault()?.Name,
                Direction = positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.Name,
                DirectionId = (int)positions.Where(ps => ps.Childs.Any(pps => pps.Id == el.Positions.FirstOrDefault()?.Id)).FirstOrDefault()?.Id,
                Salary = el.Salary,
                Comment = el.Comment,
                Comment2 = el.Comment2,
                Quality = el.Quality.ToString(),
                RevisionDate = el.RevisionDate,
                Values = string.Join(", ", el.Values.Select(v => v.Name))
            }).ToList();

            return result;
        }
    }
}
