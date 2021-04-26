using System;

namespace SmartstaffApp.Models
{
    public class StaffVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phones { get; set; }
        public string Groups { get; set; }
        public bool? Female { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime FirstWorkingDate { get; set; }
        public string Skype { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int PositionId { get; set; }
        public string Direction { get; set; }
        public int DirectionId { get; set; }
        public bool IsTargetDirection { get; set; }
        public bool DirectionHasRO { get; set; }
        public bool IsActive { get; set; }
        public DateTime? NotActiveDate { get; set; }
        public bool IsArived { get; set; }
        public DateTime? ArivedDate { get; set; }
    }
}
