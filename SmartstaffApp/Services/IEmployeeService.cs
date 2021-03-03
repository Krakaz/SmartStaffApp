using SmartstaffApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartstaffApp.Services
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeVM>> GetAllEmployeesAsync(CancellationToken cancellationToken);
    }
}
