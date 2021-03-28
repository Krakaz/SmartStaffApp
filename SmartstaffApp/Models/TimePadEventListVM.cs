using System;

namespace SmartstaffApp.Models
{
    /// <summary>
    /// Модель представления для событий TimePad
    /// </summary>
    public class TimePadEventListVM
    {
        /// <summary>
        /// Дата и время мероприятия
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Наименование мероприятия
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на мероприятие на TimePad
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Город проведения
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Адрес проведения
        /// </summary>
        public string Address { get; set; }
    }
}
