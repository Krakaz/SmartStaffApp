using System.Collections.Generic;

namespace DataLoader.Maketalents.Models
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string portalId { get; set; }
    }

    public class EnglishLevelSub
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Position
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class SourceStaff
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string email { get; set; }
        public List<string> phones { get; set; }
        public List<Group> groups { get; set; }
        public bool? female { get; set; }
        public string birthday { get; set; }
        public string firstWorkingDate { get; set; }
        public string skype { get; set; }
        public bool disabled { get; set; }
        public List<string> englishLevel { get; set; }
        public List<string> englishLevelHum { get; set; }
        public List<EnglishLevelSub> englishLevelSub { get; set; }
        public string fullName { get; set; }
        public List<Position> positions { get; set; }
        public object perspectiveComment { get; set; }
        public int perspective { get; set; }
        public string city { get; set; }
        public bool leader { get; set; }
        public bool fired { get; set; }
    }
}
