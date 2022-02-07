namespace Automated.Testing.System.Analytics.Entities.Models
{
    /// <summary>
    /// Информация о количестве выполненых задач в категории
    /// </summary>
    public sealed class ExecuteTaskInfo
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