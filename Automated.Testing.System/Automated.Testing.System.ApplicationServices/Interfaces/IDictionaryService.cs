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
    }
}