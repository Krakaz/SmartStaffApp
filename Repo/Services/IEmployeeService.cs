using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получает информацию по сотрудникам
        /// </summary>
        Task<IList<Employee>> GetAllAsync(CancellationToken cancellationToken);
    }
}
