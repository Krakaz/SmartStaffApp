using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartstaffApp.Models
{
    public class ApplicantVM
    {
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
