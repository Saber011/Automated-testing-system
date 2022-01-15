namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    /// <summary>
    /// Задачи теста
    /// </summary>
    public sealed class TestTask
    {
        /// <summary>
        /// Id задачи
        /// </summary>
        public int TestTaskId { get; set; }
        
        /// <summary>
        /// id теста
        /// </summary>
        public int TestId { get; set; }
        
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Тип задачи
        /// </summary>
        public int TypeId { get; set; }
    }
}