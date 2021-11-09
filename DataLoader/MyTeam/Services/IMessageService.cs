using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.MyTeam.Services
{
    /// <summary>
    /// Сервис работы с сообщениями
    /// </summary>
    public interface IMessageService
    {
        Task SendMessage(string chatId, string text, CancellationToken cancellationToken);

    }
}
