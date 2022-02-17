using System;
using System.Threading.Tasks;
using Automated.Testing.System.Analytics.Entities.Models;

namespace Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories
{
     public interface IStatisticRepository
    {
        /// <summary>
        /// Получить количество созданых тестов за период
        /// </summary>
        Task<TestStatistic[]> GetTestStatistic(DateTime startDate, DateTime endDate, bool deleteStatistic = false);

        /// <summary>
        /// Получить статистику по пройденным тестам в каждой категории.
        /// </summary>
        Task<ExecuteTaskInfo[]> GetCompletedTestsOnCategory();

        /// <summary>
        /// Получить активность пользователя
        /// </summary>
        Task<UserExecuteTestInfo[]> GetUserActivity(int userId);
        
        /// <summary>
        /// Получить самого активного пользователя
        /// </summary>
        Task<ActiveUserInfo[]> GetMostActivityUser();
    }
}
