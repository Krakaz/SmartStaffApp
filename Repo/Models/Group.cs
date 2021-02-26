using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repo.Models
{
    /// <summary>
    /// Принадлежность к группе сотрудников
    /// </summary>
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public List<Staff> Staffs { get; set; } = new List<Staff>();
    }
}
