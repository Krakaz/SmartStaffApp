using SmartstaffApp.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartstaffApp.Services
{
    /// <summary>
    /// Интерфейс логики работы с соискателями
    /// </summary>
    public interface IApplicantsService
    {
        /// <summary>
        /// Получает всех соискателей из БД
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<ApplicantVM>> GetApplicantsListAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Загружает информацию по соискателям в БД
        /// </summary>
        Task LoafApplicantsAsync(CancellationToken cancellationToken);
    }
}
