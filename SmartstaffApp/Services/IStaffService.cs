using SmartstaffApp.Models;
using SmartstaffApp.Pages;
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
        Task<IList<DetailInformationByMonth>> GetDetailInformationByMonthAsync(InterviewFilter filter, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получает прирост сотрудников по направлениям и месяцам
        /// </summary>
        Task<TotalGrowByMonthAndDirection> GetTotalGrowByMonthAndDirectionAsync(int year, CancellationToken cancellationToken);

        /// <summary>
        /// Получает прирост сотрудников по направлениям и месяцам по филиалу или стриму
        /// </summary>
        Task<TotalGrowByMonthAndDirection> GetTotalGrowByMonthDirectionBranchAsync(int year, int branchId, CancellationToken cancellationToken);

        /// <summary>
        /// Получает список сотрудников компании
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<StaffVM>> GetStaffAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает список сотрудников компании городу проживания
        /// </summary>
        Task<IList<StaffVM>> GetStaffByBranchIdAsync(int branchId, CancellationToken cancellationToken);

        /// <summary>
        /// Получает список всех направлений и должностей
        /// </summary>
        Task<IList<Repo.Models.Position>> GetPositionsAsync(CancellationToken cancellationToken);
    }
}
