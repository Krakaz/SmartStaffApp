using Repo.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    /// <summary>
    /// Сервис работы с городами
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Создает новый город
        /// </summary>
        Task<City> InsertAsync(City city, CancellationToken cancellationToken);

        /// <summary>
        /// Получает город по имени
        /// </summary>
        Task<City> GetByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получает список городов
        /// </summary>
        Task<List<City>> GetListAsync(CancellationToken cancellationToken);
    }
}
