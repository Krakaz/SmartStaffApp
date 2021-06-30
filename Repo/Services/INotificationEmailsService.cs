using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface INotificationEmailsService
    {
        /// <summary>
        /// Получает список всех Email для нотификации
        /// </summary>
        Task<IList<string>> GetAllAsync(CancellationToken cancellationToken);
    }
}
