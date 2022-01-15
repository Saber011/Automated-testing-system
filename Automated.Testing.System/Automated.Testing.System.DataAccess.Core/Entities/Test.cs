namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    /// <summary>
    /// Тест
    /// </summary>
    public sealed class Test
    {
        /// <summary>
        /// id
        /// </summary>
        public int TestId { get; set; }
        
        /// <summary>
        /// Категория теста
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Название теста
        /// </summary>
        public string Name { get; set; }
    }
}