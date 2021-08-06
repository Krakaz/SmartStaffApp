using System;
using System.Collections.Generic;

namespace SmartstaffApp.Models
{
    public class StaffTurnoverByMonth
    {
        public int DirectionId { get; set; }
        public string DirectionName { get; set; }
        public IList<StaffTurnoverDetail> StaffTurnoverDetails { get; set; } = new List<StaffTurnoverDetail>();
    }

    public class StaffTurnoverDetail
    {
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public int StartCount { get; set; }
        public int EndCount { get; set; }
        public int FiredCount { get; set; }
        public double StaffTurnover { get { return Math.Round(1.0 * FiredCount / (1.0 * (StartCount + EndCount) / 2.0), 2); } }
    }
}
