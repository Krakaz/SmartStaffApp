using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    /// <summary>
    /// Сервис работы с персоналом
    /// </summary>
    public interface IStaffService
    {
        /// <summary>
        /// Создает новую группу
        /// </summary>
        Task<int> InsertAsync(Staff staff, CancellationToken cancellationToken);

        /// <summary>
        /// Получает группу по идентификатору
        /// </summary>
        Task<Staff> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает список активных сотрудников
        /// </summary>
        Task<IList<Staff>> GetActiveAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает список активных сотрудников по городу
        /// </summary>
        Task<IList<Staff>> GetActiveByCityIdAsync(int cityId, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет сотрудника
        /// </summary>
        Task<Staff> UpdateAsync(Staff staff, CancellationToken cancellationToken);

        /// <summary>
        /// Получает информацию по всем сотрудникам
        /// </summary>
        Task<IList<Staff>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает информацию по сотрудникам по городу
        /// </summary>
        Task<IList<Staff>> GetAllByCityIdAsync(int cityId, CancellationToken cancellationToken);
    }
}
