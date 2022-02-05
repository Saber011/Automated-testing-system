namespace Automated.Testing.System.Common.Test.Dto.Request
{
    /// <summary>
    /// Запрос на создание теста
    /// </summary>
    public sealed class CreateTestRequest
    {
        /// <summary>
        /// Наименование теста
        /// </summary>
        public string TestName { get; set; }
        
        /// <summary>
        /// Категория теста
        /// </summary>
        public int[] CategoryIds { get; set; }
        
        /// <summary>
        /// Задачи теста
        /// </summary>
        public TestTask[] Task { get; set; }
    }
}