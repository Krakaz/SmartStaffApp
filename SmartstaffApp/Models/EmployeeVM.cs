using Repo.Models;
using System;

namespace SmartstaffApp.Models
{
    /// <summary>
    /// Класс сотрудника
    /// </summary>
    public class EmployeeVM: StaffVM
    {
        /// <summary>
        /// Текущая ЗП
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Кваллификация
        /// </summary>
        public string Quality { get; set; }
        /// <summary>
        /// Ориентировочная дата следующего пересмотра
        /// </summary>
        public DateTime RevisionDate { get; set; }
        /// <summary>
        /// Комментарий 1
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Комментарий 2
        /// </summary>
        public string Comment2 { get; set; }
        /// <summary>
        /// Ценность сотрудника
        /// </summary>
        public string Values { get; set; }
    }
}
