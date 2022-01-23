using System.Threading.Tasks;
using Automated.Testing.System.DataAccess.Abstractions.Entities;

namespace Automated.Testing.System.DataAccess.Abstractions.Interfaces
{
    public interface IDictionaryRepository
    {
        /// <summary>
        /// Получить все справочники.
        /// </summary>
        Task<Dictionary[]> GetAllDictionaryAsync();
        
        /// <summary>
        /// Получить элементы словаря по id справочника.
        /// </summary>
        Task<DictionaryItem[]> GetDictionaryElementsByDictionaryIdAsync(int id);

        /// <summary>
        /// Создать новый элемент справочника.
        /// </summary>
        Task<bool> CreateDictionaryItemAsync(string tableName, string dictionaryElementName);
        
        /// <summary>
        /// Изменить элемент справочника.
        /// </summary>
        Task<bool> UpdateDictionaryItemAsync(string tableName, int dictionaryElementId, string dictionaryElementName);
        
        /// <summary>
        /// Удалить элемент справочника.
        /// </summary>
        Task<bool> DeleteDictionaryItemAsync(string tableName, int dictionaryElementId);

        /// <summary>
        /// Получить физическую таблицу.
        /// </summary>
        Task<string> GetDictionaryTableNameAsync(int id);
        
        /// <summary>
        /// Получить статьи
        /// </summary>
        Task<Article[]> GetArticlesAsync(int[]? categoryIds, string? title, int pageSize, int pageNumber);

        /// <summary>
        /// Создать новую статью
        /// </summary>
        Task<bool> CreateArticleAsync(string title, string text, int[] categoryIds);
        
        /// <summary>
        /// Обновить статью
        /// </summary>
        Task<bool> UpdateArticleAsync(int articleId, string title, string text, int[] categoryIds);
        
        /// <summary>
        /// Удалить статью
        /// </summary>
        Task<bool> DeleteArticleAsync(int articleId);
    }
}