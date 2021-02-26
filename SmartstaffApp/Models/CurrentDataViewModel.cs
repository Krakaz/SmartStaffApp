namespace SmartstaffApp.Models
{
    /// <summary>
    /// Текущие данные по сотрудникам
    /// </summary>
    public class CurrentDataViewModel
    {
        /// <summary>
        /// Количество сотрудников
        /// </summary>
        public int CurrentCount { get; set; }

        /// <summary>
        /// Первое целевое состояние
        /// </summary>
        public int FirstTargetCount { get; set; }

        /// <summary>
        /// На сколько нужно увеличить штат, что бы достичь первого целевого состояния
        /// </summary>
        public int FirstTargetDelta { get { return FirstTargetCount - CurrentCount; } }

        /// <summary>
        /// Второе целевое состояние
        /// </summary>
        public int SecondTargetCount { get; set; }

        /// <summary>
        /// На сколько нужно увеличить штат, что бы достичь второго целевого состояния
        /// </summary>
        public int SecondTargetDelta { get { return SecondTargetCount - CurrentCount; } }

        /// <summary>
        /// Целевое состояние на конец года
        /// </summary>
        public int YearTargetCount { get; set; }

        /// <summary>
        /// На сколько нужно увеличить штат, что бы достичь целевого состояния на конец года
        /// </summary>
        public int YearTargetDelta { get { return YearTargetCount - CurrentCount; } }
    }
}
