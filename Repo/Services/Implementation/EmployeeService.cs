using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Models;

namespace Repo.Services.Implementation
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly RepoContext repoContsext;

        public EmployeeService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }
        public async Task<IList<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.repoContsext.Employees.Include(el => el.Positions).Include(el => el.Values).ToListAsync();
        }
    }
}
