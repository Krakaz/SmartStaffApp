using DataLoader.Maketalents.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.Maketalents.Services
{
    /// <summary>
    /// Сервис загрузки данных из системы МТ
    /// </summary>
    public interface IMaketalentsService
    {
        /// <summary>
        /// Загружает информацию по проведенным и запланированным интервью
        /// </summary>
        Task LoadIntervievInformationAsync(int year, CancellationToken cancellationToken);

        /// <summary>
        /// Загружает информацию по новым сотрудникам
        /// </summary>
        Task LoadNewStaffAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Загружает информацию по уволенным сотрудникам
        /// </summary>
        Task UpdateFiredStaffAsync(int year, CancellationToken cancellationToken);

        /// <summary>
        /// Получает список соискателей с нужными нам статусами
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ApplicantsResponce> LoadApplicantsAsync(CancellationToken cancellationToken);
    }
}
