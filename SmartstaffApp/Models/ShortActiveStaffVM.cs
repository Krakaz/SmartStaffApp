namespace SmartstaffApp.Models
{
    /// <summary>
    /// Класс для отображение активных сотрудников по направлениям
    /// </summary>
    public class ShortActiveStaffVM
    {
        public int DirectionId { get; set; }
        public string DirectionName { get; set; }
        public int StaffCount { get; set; }
        public bool IsTarget { get; set; }
        public bool HasRO { get; set; }
    }
}
