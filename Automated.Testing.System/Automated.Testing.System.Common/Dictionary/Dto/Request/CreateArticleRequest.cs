namespace Automated.Testing.System.Common.Dictionary.Dto.Request
{
    /// <summary>
    /// Создать новую статью
    /// </summary>
    public class CreateArticleRequest
    {
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