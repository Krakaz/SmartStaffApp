using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repo.Models
{
    /// <summary>
    /// Позиция сотрудника
    /// </summary>
    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        /// <summary>
        /// Дочерние позиции
        /// </summary>
        public List<Position> Childs { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Целевое направление
        /// </summary>
        public bool IsTarget { get; set; }
        /// <summary>
        /// В направлении есть руководитель отдела
        /// </summary>
        public bool HasRO { get; set; }
        public List<Staff> Staffs { get; set; } = new List<Staff>();
    }

}
