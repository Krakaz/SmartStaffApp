using Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    /// <summary>
    /// Сервис работы с соискателями
    /// </summary>
    public interface IApplicantService
    {
        /// <summary>
        /// Добавляет запись о соискателе
        /// </summary>
        Task<Applicant> InsertAsyc(Applicant applicant, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет запись о соискателе
        /// </summary>
        Task DeleteAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет запись о соискателе
        /// </summary>
        Task<Applicant> UpdateAsync(Applicant applicant, CancellationToken cancellationToken);

        /// <summary>
        /// Получает список всех соискателей
        /// </summary>
        Task<IList<Applicant>> GetApplicantsAsync(CancellationToken cancellationToken);
    }
}
