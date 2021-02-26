using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    /// <summary>
    /// Сервис работы с позициями разработчиков
    /// </summary>
    public interface IPositionService
    {
        /// <summary>
        /// Создает новую группу
        /// </summary>
        Task<int> InsertAsync(Position position, CancellationToken cancellationToken);

        /// <summary>
        /// Получает группу по идентификатору
        /// </summary>
        Task<Position> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает все должности в виде дерева
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<Position>> GetAllAsync(CancellationToken cancellationToken);
    }
}
