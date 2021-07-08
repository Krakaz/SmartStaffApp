using System.Collections.Generic;
using System.Linq;

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

        private int incomingCnt;
        public new int IncomingCnt { get { return Childs.Count() == 0 ? incomingCnt : Childs.Sum(el => el.IncomingCnt); } set { this.incomingCnt = value; } }

        private int firedCnt;
        public new int FiredCnt { get { return Childs.Count() == 0 ? firedCnt : Childs.Sum(el => el.FiredCnt); } set { this.firedCnt = value; } }

        private int arivedCnt;
        public new int ArivedCnt { get { return Childs.Count() == 0 ? arivedCnt : Childs.Sum(el => el.ArivedCnt); } set { this.arivedCnt = value; } }

        private int interviewCnt;
        public new int InterviewCnt { get { return Childs.Count() == 0 ? interviewCnt : Childs.Sum(el => el.InterviewCnt); } set { this.interviewCnt = value; } }

        /// <summary>
        /// Прирост сотрудников за месяц
        /// </summary>
        public new int CulculateCount { get { return IncomingCnt + ArivedCnt - FiredCnt; } }

        /// <summary>
        /// Результативность интрвью
        /// </summary>
        public new int ResultativityCnt
        {
            get
            {
                if (InterviewCnt == 0)
                {
                    return 0;
                }
                else
                {
                    return (int)100.0 * IncomingCnt / InterviewCnt;
                }
            }
        }

        public IList<DetailInformationByMonth> Childs { get; set; } = new List<DetailInformationByMonth>();
    }


}
