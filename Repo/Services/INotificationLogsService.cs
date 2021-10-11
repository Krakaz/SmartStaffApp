using Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services
{
    public interface INotificationLogsService
    {
        Task InsertAsync(NotificationType notificationType, CancellationToken cancellationToken);
        Task InsertBirthdayLogAsync(CancellationToken cancellationToken);

        Task<DateTime> GetLastDateAsync(NotificationType notificationType, CancellationToken cancellationToken);

        Task<DateTime> GetLastDateBirthdayNotificationAsync(CancellationToken cancellationToken);
    }
}
