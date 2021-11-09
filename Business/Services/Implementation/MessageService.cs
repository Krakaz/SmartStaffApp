using Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services.Implementation
{
    internal class MessageService : IMessageService
    {
        private readonly Repo.Services.INotificationEmailsService notificationEmailsService;
        private readonly DataLoader.MyTeam.Services.IMessageService myTeamMessageService;

        public MessageService(Repo.Services.INotificationEmailsService notificationEmailsService,
            DataLoader.MyTeam.Services.IMessageService myTeamMessageService)
        {
            this.notificationEmailsService = notificationEmailsService;
            this.myTeamMessageService = myTeamMessageService;
        }

        public async Task SendMessageAsync(MessageType messageType, string text, CancellationToken cancellationToken)
        {
            var emails = await this.notificationEmailsService.GetAllAsync(cancellationToken);
            foreach (var email in emails.Where(el => el.NotificationTypes.Any(tp => tp.Id == (int)messageType)))
            {
                await this.myTeamMessageService.SendMessage(email.Email, text, cancellationToken);
            }
        }
    }
}
