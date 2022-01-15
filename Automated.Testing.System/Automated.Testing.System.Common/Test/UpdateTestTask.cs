namespace Automated.Testing.System.Common.Test
{
    /// <summary>
    /// Обновление задачи
    /// </summary>
    public sealed class UpdateTestTask
    {
        /// <summary>
        /// id задачи
        /// </summary>
        public int TestTaskId { get; set; }
        
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