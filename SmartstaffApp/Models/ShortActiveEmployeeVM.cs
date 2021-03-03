namespace SmartstaffApp.Models
{
    /// <summary>
    /// Класс для отображение активных сотрудников в направлении
    /// </summary>
    public class ShortActiveEmployeeVM
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int StaffCount { get; set; }
    }
}
