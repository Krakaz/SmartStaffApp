using SmartstaffApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartstaffApp.Services
{
    public interface IStaffService
    {
        Task<CurrentDataViewModel> GetCurrentDataAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает сводную информацию по найму сотрудников по месяцам за выбранный год
        /// </summary>
        Task<IList<InformationByMonth>> GetInformationByMonthAsync(int year, CancellationToken cancellationToken);

        /// <summary>
        /// Получает сводную информацию по найму сотрудников по месяцам за выбранный год с делением на позиции
        /// </summary>
        Task<IList<DetailInformationByMonth>> GetDetailInformationByMonthAsync(bool isShortView, int year, CancellationToken cancellationToken);


        /// <summary>
        /// Получает список сотрудников компании
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<StaffVM>> GetStaffAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает список всех направлений и должностей
        /// </summary>
        Task<IList<Repo.Models.Position>> GetPositionsAsync(CancellationToken cancellationToken);
    }
}
