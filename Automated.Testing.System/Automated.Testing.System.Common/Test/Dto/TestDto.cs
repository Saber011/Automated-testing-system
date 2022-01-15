namespace Automated.Testing.System.Common.Test.Dto
{
    /// <summary>
    /// Тест
    /// </summary>
    public sealed class TestDto
    {
        /// <summary>
        /// id теста
        /// </summary>
        public int TestId { get; set; }
        
        /// <summary>
        /// Категория
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Название теста
        /// </summary>
        public string Name { get; set; }
    }
}