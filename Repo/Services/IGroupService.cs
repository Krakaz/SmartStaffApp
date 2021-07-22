using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    /// <summary>
    /// Сервис работы с группами
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Создает новую группу
        /// </summary>
        Task<int> InsertAsync(Group group, CancellationToken cancellationToken);

        /// <summary>
        /// Получает группу по идентификатору
        /// </summary>
        Task<Group> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает филиалы и стримы
        /// </summary>
        Task<IList<Group>> GetBranchesAsync(CancellationToken cancellationToken);

    }
}
