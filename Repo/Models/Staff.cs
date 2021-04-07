using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repo.Models
{
    public class Staff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phones { get; set; } 
        public List<Group> Groups { get; set; } = new List<Group>();
        public bool? Female { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime FirstWorkingDate { get; set; }
        public string Skype { get; set; }
        public string FullName { get; set; }

        public List<Position> Positions { get; set; } = new List<Position>();

        public bool IsActive { get; set; }
        public DateTime? NotActiveDate { get; set; }
        public bool IsArived { get; set; }
        public DateTime? ArivedDate { get; set; }
        public City City { get; set; }
    }

}
