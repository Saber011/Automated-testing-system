using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Entities;

namespace Automated.Testing.System.DataAccess.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// Получить все задания теста.
        /// </summary>
        Task<TestTask[]> GetTestTaskAsync(int testId);

        /// <summary>
        ///  Получить варианты ответа для задачи.
        /// </summary>
        Task<(int taskId, int optionId, string option)[]> GetTestTaskResponseOptionAsync(int[] taskIds);
        
        /// <summary>
        /// Получить все тесты.
        /// </summary>
        Task<Test[]> GetTestsAsync(int[] categoryIds);

        /// <summary>
        /// Проверить результат теста.
        /// </summary>
        Task<dynamic> CheckTestResultsAsync(dynamic request);
        
        /// <summary>
        /// Создать тест
        /// </summary>
        Task<int> CreateTestAsync(string name, int userId);
        
        /// <summary>
        /// Добавить связь теста и категории
        /// </summary>
        Task<bool> CreateTestCategoryAsync(int testId, int[] categoryIds);

        /// <summary>
        /// Добавить задачи для теста.
        /// </summary>
        Task<int> CreateTestTaskAsync(string taskName, int testId, int testType);
        
        /// <summary>
        /// Добавить варианты ответа для задачи теста.
        /// </summary>
        Task<bool> CreateTestTaskResponseOptionAsync(string[] options, int testTaskId);

        /// <summary>
        /// Добавить варианты ответа для теста
        /// </summary>
        Task<bool> CreateTestTaskAnswersAsync(string[] answers, int testTaskId);

        /// <summary>
        /// Удалить тест
        /// </summary>
        Task<bool> RemoveTestAsync(int id);
        
        /// <summary>
        /// Изменить тест.
        /// </summary>
        Task<bool> UpdateTestAsync(int testId, string name, int categoryId);

        /// <summary>
        /// Удалить всю дополнительную информацию по тесту
        /// </summary>
        Task<bool> DeleteTestInformationTaskAsync(int testId);
    }
}