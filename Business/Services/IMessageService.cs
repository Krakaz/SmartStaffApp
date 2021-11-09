using Business.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(MessageType messageType, string text, CancellationToken cancellationToken);
    }
}
