using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class NotificationLog
    {
        public int Id { get; set; }
        public DateTime MyProperty { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
