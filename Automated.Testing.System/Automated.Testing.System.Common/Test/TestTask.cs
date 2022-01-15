namespace Automated.Testing.System.Common.Test
{
    /// <summary>
    /// Задача теста
    /// </summary>
    public sealed class TestTask
    {
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Тип задачи
        /// </summary>
        public int TypeId { get; set; }
        
        /// <summary>
        /// Варианты ответа задачи
        /// </summary>
        public string[] ResponseOptions { get; set; }
        
        /// <summary>
        /// Ответы
        /// </summary>
        public string[] Answers { get; set; }
    }
}