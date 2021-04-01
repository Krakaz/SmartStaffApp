using System.Collections.Generic;

namespace SmartstaffApp.Models
{
    /// <summary>
    /// Прирост направления по месяцам по направлениям
    /// </summary>
    public class TotalGrowByMonthAndDirection
    {
        /// <summary>
        /// Направления
        /// </summary>
        public IList<DirectionVM> Header { get; set; } = new List<DirectionVM>();

        /// <summary>
        /// Значения
        /// </summary>
        public IList<TotalGrowByMonthAndDirectionValues> Values { get; set; } = new List<TotalGrowByMonthAndDirectionValues>();
    }

    public class TotalGrowByDirection
    {
        /// <summary>
        /// Идентификатор направления
        /// </summary>
        public int DirectionId { get; set; }

        /// <summary>
        /// Прирост сотрудников за месяц
        /// </summary>
        public int TotalGrowCount { get; set; }
    }

    public class TotalGrowByMonthAndDirectionValues
    {
        /// Месяц рассчета
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Название месяца
        /// </summary>
        public string MonthName { get; set; }

        /// <summary>
        /// Значения
        /// </summary>
        public IList<TotalGrowByDirection> TotalGrowByDirection { get; set; } = new List<TotalGrowByDirection>();
    }
}
