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
        public List<Position> Childs { get; set; }
        public string Name { get; set; }
        public List<Staff> Staffs { get; set; } = new List<Staff>();
    }

}
