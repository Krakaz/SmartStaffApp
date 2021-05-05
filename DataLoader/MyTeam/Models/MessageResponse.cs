using System;
using System.Collections.Generic;
using System.Text;

namespace DataLoader.MyTeam.Models
{
    /// <summary>
    /// Класс ответа сервера на отправку сообщения
    /// </summary>
    public class MessageResponse
    {
        public long MsgId { get; set; }
        public bool Ok { get; set; }
    }
}
