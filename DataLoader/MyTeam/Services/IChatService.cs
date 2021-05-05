using DataLoader.MyTeam.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLoader.MyTeam.Services
{
    /// <summary>
    /// Сервис работы с чатами MyTeam
    /// </summary>
    public interface IChatService
    {
        Task<ChatMembers> GetMainChatMembersAsync(CancellationToken cancellationToken);
        Task<ChatMembers> GetMainChanalMembersAsync(CancellationToken cancellationToken);
    }
}
