namespace Automated.Testing.System.DataAccess.Postgres.Entities
{
    /// <summary>
    /// Информация о з/п.
    /// </summary>
    public sealed class WorkerSalary
    {
        /// <summary>
        /// Номер строки.
        /// </summary>
        public int NumRow { get; set; }
        
        /// <summary>
        /// Почасовая оплата.
        /// </summary>
        public bool IsHourlyPayment { get; set; }
        
        /// <summary>
        /// Фамилия и Имя.
        /// </summary>
        public string NameAndSurname { get; set; }
        
        /// <summary>
        /// Оплата труда.
        /// </summary>
        public int Salary { get; set; }
        
        /// <summary>
        /// Количество отработанных часов.
        /// </summary>
        public int WorkHour { get; set; }
        
        /// <summary>
        /// Доплата за разряд.
        /// </summary>
        public int AmountSurcharge { get; set; }
    }
}