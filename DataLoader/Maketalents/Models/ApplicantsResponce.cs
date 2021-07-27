using System;
using System.Collections.Generic;
using System.Text;

namespace DataLoader.Maketalents.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Entity
    {
        public bool approved { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public int id { get; set; }
        public long lastActivity { get; set; }
        public int lastActivityId { get; set; }
        public string lastActivityName { get; set; }
        public string lastActivityStatus { get; set; }
        public string lastActivityType { get; set; }
        public string phone { get; set; }
        public object carNum { get; set; }
        public List<string> positions { get; set; }
        public List<object> groups { get; set; }
        public string status { get; set; }
        public string statusColor { get; set; }
        public List<object> technologies { get; set; }
        public List<string> englishLevel { get; set; }
        public bool hipo { get; set; }
        public int perspective { get; set; }
        public int starRating { get; set; }
        public object perspectivecomment { get; set; }
        public object mark { get; set; }
        public object vacancyAssignDate { get; set; }
        public object jobBeforeOurCompany { get; set; }
        public bool notReadyForContact { get; set; }
        public object highSchoolCourse { get; set; }
    }

    public class ApplicantsResponce
    {
        public List<Entity> entities { get; set; }
        public int filteredSize { get; set; }
        public int totalSize { get; set; }
    }
}
