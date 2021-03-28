using DataLoader.Timepad.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.Timepad.Services
{
    /// <summary>
    /// Сервис для загрузки данных с TimePad
    /// </summary>
    public interface ITimePadEventService
    {
        /// <summary>
        /// Получает список событий компании SimbirSoft с TimePad
        /// </summary>
        Task<EventList> GetEventListAsync(CancellationToken cancellationToken);
    }
}
