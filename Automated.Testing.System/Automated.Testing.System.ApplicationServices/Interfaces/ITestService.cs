using System.Threading.Tasks;

namespace Automated.Testing.System.ApplicationServices.Interfaces
{
    /// <summary>
    /// Сервис для работы с тестами.
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// Получить все задания теста.
        /// </summary>
        Task<dynamic[]> GetTestTaskAsync(int testId);
        
        /// <summary>
        /// Получить все тесты.
        /// </summary>
        Task<dynamic[]> GetTestsAsync(int? categoryId);

        /// <summary>
        /// Проверить результат теста.
        /// </summary>
        Task<dynamic> CheckTestResultsAsync(dynamic request);
    }
}