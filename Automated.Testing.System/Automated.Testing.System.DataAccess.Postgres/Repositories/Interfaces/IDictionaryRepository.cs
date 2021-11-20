using System.Threading.Tasks;
using Automated.Testing.System.DataAccess.Postgres.Entities;

namespace Automated.Testing.System.DataAccess.Postgres.Repositories.Interfaces
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
        Task<string> GetDictionaryTableName(int id);
    }
}