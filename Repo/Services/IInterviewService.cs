using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    /// <summary>
    /// Сервис работы с таблицей интервью
    /// </summary>
    public interface IInterviewService
    {
        /// <summary>
        /// Апсертит информацию по интервью
        /// </summary>
        Task UpsertAsync(IList<Interview> interviews, int year, CancellationToken cancellationToken);

        /// <summary>
        /// Получает информацию по интервью за выбранный год
        /// </summary>
        Task<IList<Interview>> GetByYear(int year, CancellationToken cancellationToken);
    }
}
