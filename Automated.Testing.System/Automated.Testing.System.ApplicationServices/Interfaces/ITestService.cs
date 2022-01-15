using System.Threading.Tasks;
using Automated.Testing.System.Common.Test.Dto;
using Automated.Testing.System.Common.Test.Dto.Request;

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
        Task<TestTaskDto[]> GetTestTaskAsync(int testId);
        
        /// <summary>
        /// Получить все тесты.
        /// </summary>
        Task<TestDto[]> GetTestsAsync(int? categoryId);
        
        /// <summary>
        /// Получить ответы для теста.
        /// </summary>
        Task<TestDto[]> GetTestAnswerAsync(int testId);

        /// <summary>
        /// Проверить результат теста.
        /// </summary>
        Task<dynamic> CheckTestResultsAsync(dynamic request);
        
        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<bool> CreateTestAsync(CreateTestRequest request);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<bool> RemoveTestAsync(int testId);

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        Task<bool> UpdateTestAsync(UpdateTestRequest request);
    }
}