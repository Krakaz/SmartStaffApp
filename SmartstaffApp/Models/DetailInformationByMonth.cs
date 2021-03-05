using System.Collections.Generic;

namespace SmartstaffApp.Models
{
    /// <summary>
    /// Детальная информация по месяцам
    /// </summary>
    public class DetailInformationByMonth: InformationByMonth
    {

        /// <summary>
        /// Наименование родительской позиции(отдела)
        /// </summary>
        public string ParentPosition { get; set; }

        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Position { get; set; }


        public IList<DetailInformationByMonth> Childs { get; set; } = new List<DetailInformationByMonth>();
    }


}
