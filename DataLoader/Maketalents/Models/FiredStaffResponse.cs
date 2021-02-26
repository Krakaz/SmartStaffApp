using Newtonsoft.Json;
using System;
namespace DataLoader.Maketalents.Models
{
    /// <summary>
    /// Класс для получения уволенных
    /// </summary>
    public class FiredStaffResponse
    {
        public long date { get; set; }
        public string email { get; set; }
        public string groups { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
