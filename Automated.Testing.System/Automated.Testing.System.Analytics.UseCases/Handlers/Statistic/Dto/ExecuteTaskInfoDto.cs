namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto
{
    /// <summary>
    /// Информация о количестве выполненых задач в категории
    /// </summary>
    public sealed class ExecuteTaskInfoDto
    {
        /// <summary>
        /// Наименование категории
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Количество правильно выполненых задач
        /// </summary>
        public int CountCorrect { get; set; }
    }
}