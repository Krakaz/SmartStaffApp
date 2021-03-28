namespace DataLoader.Timepad.Models
{
    public class EventDetails
    {
        public int id { get; set; }
        public string starts_at { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string moderation_status { get; set; }
        public Location location { get; set; }
    }
}