namespace Automated.Testing.System.Common.Dictionary.Dto.Request
{
    /// <summary>
    /// Запрос на обновление элемента справочника.
    /// </summary>
    public class UpdateDictionaryElementRequest
    {
        /// <summary>
        /// Id словаря.
        /// </summary>
        public int DictionaryId { get; set; }
        
        /// <summary>
        /// Id элемента.
        /// </summary>
        public int ElementId { get; set; }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }
    }
}