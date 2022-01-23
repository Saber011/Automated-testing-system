namespace Automated.Testing.System.Common.Dictionary.Dto.Request
{
    /// <summary>
    /// Получить статьи
    /// </summary>
    public class GetArticlesRequest
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string? Title { get; set; }
        
        /// <summary>
        /// Категории
        /// </summary>
        public int[]? CategoryIds { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; } = 25;

        /// <summary>
        /// Текущая странциа
        /// </summary>
        public int PageNumber { get; set; } = 1;
    }
}