using Microsoft.EntityFrameworkCore;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repo.Services.Implementation
{
    internal class NotificationLogsService: INotificationLogsService
    {
        private readonly RepoContext repoContsext;

        public NotificationLogsService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }

        public async Task<DateTime> GetLastDateAsync(NotificationType notificationType, CancellationToken cancellationToken)
        {
            DateTime date = DateTime.Now.Date.AddDays(-1);
            
            var item = await this.repoContsext.NotificationLogs
                .Include(el => el.NotificationType)
                .Where(el => el.NotificationType.Id == notificationType.Id)
                .OrderByDescending(el => el.Date)
                .FirstOrDefaultAsync();

            if(item != null)
            {
                date = item.Date.Date;
            }

            return date;
        }

        public async Task<DateTime> GetLastDateBirthdayNotificationAsync(CancellationToken cancellationToken)
        {
            var notificationType = this.repoContsext.NotificationTypes.Where(el => el.Id == 3).FirstOrDefault();
            return await this.GetLastDateAsync(notificationType, cancellationToken);
        }

        public async Task InsertAsync(NotificationType notificationType, CancellationToken cancellationToken)
        {
            var item = new NotificationLog { Date = DateTime.Now, NotificationType = notificationType };
            await this.repoContsext.NotificationLogs.AddAsync(item);
            await this.repoContsext.SaveChangesAsync();
        }

        public async Task InsertBirthdayLogAsync(CancellationToken cancellationToken)
        {
            var notificationType = this.repoContsext.NotificationTypes.Where(el => el.Id == 3).FirstOrDefault();
            await this.InsertAsync(notificationType, cancellationToken);
        }
    }
}
