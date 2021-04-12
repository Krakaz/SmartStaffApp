using System;

namespace SmartstaffApp.Pages
{
    public class BirthdayStaffVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public int Day { get; set; }
        public int Age { get { return DateTime.Now.Year - Birthday.Year; } }
        public string  Direction { get; set; }
    }
}