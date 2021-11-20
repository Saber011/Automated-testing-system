namespace Automated.Testing.System.Common.Dictionary.Dto.Request
{
    /// <summary>
    /// Запрос на удаление элемента справочника.
    /// </summary>
    public class DeleteDictionaryElementRequest
    {
        /// <summary>
        /// Id словаря.
        /// </summary>
        public int DictionaryId { get; set; }
        
        /// <summary>
        /// Id элемента.
        /// </summary>
        public int ElementId { get; set; }
    }
}