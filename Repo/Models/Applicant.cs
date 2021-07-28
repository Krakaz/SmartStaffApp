using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repo.Models
{
    public class Applicant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public string EnglishLevel { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
