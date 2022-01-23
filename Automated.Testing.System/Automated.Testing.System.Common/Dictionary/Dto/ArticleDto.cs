namespace Automated.Testing.System.Common.Dictionary.Dto
{
    /// <summary>
    /// Статья
    /// </summary>
    public class ArticleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ArticleId { get; set; }
        
        /// <summary>
        /// Статья
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Категории
        /// </summary>
        public int[] CategoryIds { get; set; }
    }
}