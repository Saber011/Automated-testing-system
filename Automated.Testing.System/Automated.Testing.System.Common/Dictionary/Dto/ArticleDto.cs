using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Text { get; set; }
        
        /// <summary>
        /// Заголовок
        /// </summary>
        [Required]
        public string Title { get; set; }
        
        /// <summary>
        /// Категории
        /// </summary>
        public int[] CategoryIds { get; set; }
        
        /// <summary>
        /// Общее количество записей
        /// </summary>
        [Required]
        public int Total { get; set; }
    }
}