namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto
{
    /// <summary>
    /// Инофрмация о выполненых тестах у пользователя
    /// </summary>
    public sealed class UserExecuteTestInfoDto
    {
        /// <summary>
        /// Наименование теста
        /// </summary>
        public string Test { get; set; }
        
        /// <summary>
        /// Процент успеха
        /// </summary>
        public int Percent { get; set; }
        
        /// <summary>
        /// Количетсво попыток выполнить
        /// </summary>
        public int NumberAttempts { get; set; }
    }
}