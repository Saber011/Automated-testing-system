namespace Automated.Testing.System.Common.Dictionary.Dto
{
    /// <summary>
    /// Элемент словаря
    /// </summary>
    public sealed class DictionaryItemDto
    {
        /// <summary>
        /// Id элемента
        /// </summary>
        public int ElementId { get; set; }
        
        /// <summary>
        /// Наименование элемента
        /// </summary>
        public string Name { get; set; }
    }
}