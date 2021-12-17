namespace Automated.Testing.System.Common.Dictionary.Dto.Request
{
    /// <summary>
    /// Запрос на создание нового элемента словаря.
    /// </summary>
    public class CreateDictionaryElementRequest
    {
        /// <summary>
        /// Id словаря.
        /// </summary>
        public int DictionaryId { get; set; }
        
        /// <summary>
        /// Наименование элемента.
        /// </summary>
        public string Name { get; set; }
    }
}