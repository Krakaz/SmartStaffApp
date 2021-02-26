using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string PositionName { get; set; }

        public Month Month { get; set; }

        public int Year { get; set; }
        public int InterviewCount { get; set; }
    }
}
