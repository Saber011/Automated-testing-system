namespace Automated.Testing.System.Common.Test.Dto.Request
{
    /// <summary>
    /// Запрос на обновление теста
    /// </summary>
    public sealed class UpdateTestRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int TestId { get; set; }
        
        /// <summary>
        /// Наименование теста
        /// </summary>
        public string TestName { get; set; }
        
        /// <summary>
        /// Категория теста
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Задачи теста
        /// </summary>
        public UpdateTestTask[] Task { get; set; }
    }
}