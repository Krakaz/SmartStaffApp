using System;
using System.Collections.Generic;
using System.Text;

namespace DataLoader.Maketalents.Models
{
    public class ConjunctionRestriction
    {
        public string field { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }

    public class SortOrder
    {
        public string direction { get; set; }
        public string field { get; set; }
    }

    public class ApplicantsRequest
    {
        public List<ConjunctionRestriction> conjunctionRestrictions { get; set; }
        public List<object> disjunctionRestrictions { get; set; }
        public List<SortOrder> sortOrders { get; set; }
    }
}
