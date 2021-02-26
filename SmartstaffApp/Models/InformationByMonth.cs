namespace SmartstaffApp.Models
{
    /// <summary>
    /// Информация по сотрудникам по месяцам
    /// </summary>
    public class InformationByMonth
    {
        /// <summary>
        /// Месяц рассчета
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Название месяца
        /// </summary>
        public string MonthName { get; set; }
        /// <summary>
        /// Найм за месяц
        /// </summary>
        public int IncomingCnt { get; set; }
        /// <summary>
        /// Понаехи за месяц
        /// </summary>
        public int ArivedCnt { get; set; }
        /// <summary>
        /// Уволенные за месяц
        /// </summary>
        public int FiredCnt { get; set; }
        /// <summary>
        /// Прирост сотрудников за месяц
        /// </summary>
        public int CulculateCount { get { return IncomingCnt + ArivedCnt - FiredCnt; } }
        /// <summary>
        /// Количество проведенных интервью за месяц
        /// </summary>
        public int InterviewCnt { get; set; }
        /// <summary>
        /// Результативность интрвью
        /// </summary>
        public int ResultativityCnt 
        { 
            get 
            { if(InterviewCnt == 0)
                {
                    return 0;
                }
            else
                {
                    return (int) 100.0 * IncomingCnt / InterviewCnt;
                }
            } 
        }
    }
}
