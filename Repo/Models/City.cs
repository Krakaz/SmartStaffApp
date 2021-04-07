using System;
using System.Collections.Generic;
using System.Text;

namespace Repo.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Staff> Staffs { get; set; }
    }
}
