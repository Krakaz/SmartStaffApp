using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class Employee: Staff
    {
        public int Salary { get; set; }
        public Quality Quality { get; set; }
        public DateTime RevisionDate { get; set; }
        public string Comment { get; set; }
        public string Comment2 { get; set; }
        public List<Value> Values { get; set; } = new List<Value>();
    }
}
