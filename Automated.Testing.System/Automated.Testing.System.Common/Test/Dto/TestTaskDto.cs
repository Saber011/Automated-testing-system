namespace Automated.Testing.System.Common.Test.Dto
{
    /// <summary>
    /// Задания теста
    /// </summary>
    public sealed class TestTaskDto
    {
        /// <summary>
        /// Id задачи
        /// </summary>
        public int TestTaskId { get; set; }
        
        /// <summary>
        /// Id теста
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
        
        /// <summary>
        /// Варианты ответа задачи
        /// </summary>
        public ResponseOption[] ResponseOptions { get; set; }
    }
}