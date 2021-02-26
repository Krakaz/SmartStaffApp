using System.Collections.Generic;

namespace DataLoader.Maketalents.Models
{
    public class InterviewModel
    {
        public Colors colors { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class Colors
    {
        public string color { get; set; }
    }

    public class Value
    {
        public int key { get; set; }
        public int value { get; set; }
    }
}
