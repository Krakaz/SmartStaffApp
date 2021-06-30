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
    internal class NotificationEmailsService: INotificationEmailsService
    {
        private readonly RepoContext repoContsext;

        public NotificationEmailsService(RepoContext repoContsext)
        {
            this.repoContsext = repoContsext;
        }
        public async Task<IList<string>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.repoContsext.NotificationEmails.Select(el => el.Email).ToListAsync();
        }
    }
}
