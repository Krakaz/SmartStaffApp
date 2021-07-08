using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class NotificationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NotificationEmail> NotificationEmails { get; set; }
    }
}
