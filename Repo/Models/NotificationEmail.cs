using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class NotificationEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public IList<NotificationType> NotificationTypes { get; set; }
    }
}
