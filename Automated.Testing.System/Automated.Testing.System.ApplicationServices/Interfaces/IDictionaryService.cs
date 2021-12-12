using System.Threading.Tasks;

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
        Task GetAllAsync();
    }
}