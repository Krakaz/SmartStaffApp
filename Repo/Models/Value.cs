﻿using System.Collections.Generic;

namespace Repo.Models
{
    public class Value
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}