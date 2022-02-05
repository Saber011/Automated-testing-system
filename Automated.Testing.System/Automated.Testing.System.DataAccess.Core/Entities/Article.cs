namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    /// <summary>
    /// Сатья
    /// </summary>
    public class Article
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
        
        /// <summary>
        /// Количество
        /// </summary>
        public int Total { get; set; }
    }
}