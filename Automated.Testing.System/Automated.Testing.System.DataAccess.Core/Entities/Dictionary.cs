namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    /// <summary>
    /// Словарь.
    /// </summary>
    public class Dictionary
    {
        /// <summary>
        /// Id словаря
        /// </summary>
        public int DictionaryId { get; set; }
        
        /// <summary>
        /// Наименование словаря.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Таблица.
        /// </summary>
        public string TableName { get; set; }
    }
}