using System.Collections.Generic;

namespace DataLoader.MyTeam.Models
{
    public class ChatMembers
    {
        public IList<Members> members { get; set; }
        public string cursor { get; set; }
        public bool ok { get; set; }
    }

    public class Members
    {
        public string userId { get; set; }
        public bool creator { get; set; }
        public bool admin { get; set; }
    }
}
