namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto
{
    /// <summary>
    /// Статистика по количествую созданных тестов
    /// </summary>
    public sealed class TestStatisticDto
    {
        /// <summary>
        /// Год создания
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// Месяц создания
        /// </summary>
        public int Mount { get; set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
    }
}
