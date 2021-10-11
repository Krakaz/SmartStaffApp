using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartstaffApp.Services
{
    /// <summary>
    /// Сервис работы с нотификациями
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Отправляет поздравления с днем рождения
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendBirthdayNotificationAsync(CancellationToken cancellationToken);
    }
}
