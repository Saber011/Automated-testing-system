using System.Threading.Tasks;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.Dictionary.Dto.Request;

namespace Automated.Testing.System.ApplicationServices.Interfaces
{
    /// <summary>
    /// Сервис для работы со справочниками.
    /// </summary>
    public interface IDictionaryService
    {
        /// <summary>
        /// Получить все справочники.
        /// </summary>
        Task<DictionaryDto[]> GetAllAsync();
        
        /// <summary>
        /// Получить по id.
        /// </summary>
        Task<DictionaryItemDto[]> GetByIdAsync(int id);

        /// <summary>
        /// Создать новый элемент справочника.
        /// </summary>
        Task<bool> CreateDictionaryItemAsync(CreateDictionaryElementRequest request);
        
        /// <summary>
        /// Изменить элемент справочника.
        /// </summary>
        Task<bool> UpdateDictionaryItemAsync(UpdateDictionaryElementRequest request);
        
        /// <summary>
        /// Удалить элемент справочника.
        /// </summary>
        Task<bool> DeleteDictionaryItemAsync(DeleteDictionaryElementRequest request);
        
        /// <summary>
        /// Получить статьи
        /// </summary>
        Task<ArticleDto[]> GetArticlesAsync(GetArticlesRequest request);

        /// <summary>
        /// Создать новую статью
        /// </summary>
        Task<bool> CreateArticleAsync(CreateArticleRequest request);
        
        /// <summary>
        /// Обновить статью
        /// </summary>
        Task<bool> UpdateArticleAsync(UpdateArticleRequest request);
        
        /// <summary>
        /// Удалить статью
        /// </summary>
        Task<bool> DeleteArticleAsync(int articleId);
    }
}