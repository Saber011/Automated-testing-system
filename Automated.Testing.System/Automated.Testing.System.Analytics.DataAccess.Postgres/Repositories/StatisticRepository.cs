using System;
using System.Linq;
using System.Threading.Tasks;
using Automated.Testing.System.Analytics.Entities.Models;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Dapper;

namespace Automated.Testing.System.Analytics.DataAccess.Postgres.Repositories
{
    /// <inheritdoc />
    public class StatisticRepository: IStatisticRepository
    {
        private readonly IPostgresService _postgresService;

        /// <summary>
        /// Конструктор <see cref="StatisticRepository"/>.
        /// </summary>
        /// <param name="postgresService">Сервис для работы с `Postgres`.</param>
        public StatisticRepository(IPostgresService postgresService)
        {
            Guard.NotNull(postgresService, nameof(postgresService));

            _postgresService = postgresService;
        }

        /// <inheritdoc />
        public async Task<TestStatistic[]> GetTestStatistic(DateTime startDate, DateTime endDate, bool searchDelete = false)
        {
            var addDeleteFilter = searchDelete
                ? "AND is_deleted = 1"
                : string.Empty;
            var query = @$"
SELECT EXTRACT(year FROM create_date) as {nameof(TestStatistic.Year)},
       EXTRACT(MONTH FROM create_date) AS {nameof(TestStatistic.Mount)},
       COUNT(1) AS {nameof(TestStatistic.Count)}
  FROM core.test
 WHERE create_date between :startDate and :endDate
{addDeleteFilter}
GROUP BY EXTRACT(MONTH FROM create_date), EXTRACT(year FROM create_date)
ORDER BY year, mount;";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<TestStatistic>(query, new {startDate, endDate}))
                .ToArray());
        }

        /// <inheritdoc />
        public async Task<ExecuteTaskInfo[]> GetCompletedTestsOnCategory()
        {
            const string query = @$"
SELECT ct.name  as {nameof(ExecuteTaskInfo.CategoryName)},
       cnt  as {nameof(ExecuteTaskInfo.CountCorrect)}
 FROM (SELECT category_id,
       count(1) as cnt
         FROM core.test_result re
       INNER JOIN core.ref_test_category rtc on re.test_id = rtc.test_id
        WHERE re.user_response_is_correct
        GROUP BY rtc.category_id) as test
INNER JOIN core.category ct on ct.category_id = test.category_id;";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<ExecuteTaskInfo>(query))
                .ToArray());
        }

        /// <inheritdoc />
        public async Task<UserExecuteTestInfo[]> GetUserActivity(int userId)
        {
            Guard.GreaterThanZero(userId, nameof(userId));
            
            const string query = @$"
    WITH cte as (
        SELECT test_id, count(1) as cntTest, try_execute
          FROM core.test_result r
         WHERE r.user_id = :userId
      GROUP BY  test_id, try_execute
    ),
         cor as (
             SELECT test_id, count(1) as cntCore, try_execute
               FROM core.test_result r
              WHERE r.user_id = :userId and r.user_response_is_correct
           GROUP BY  test_id, try_execute
         )
SELECT t.name as {nameof(UserExecuteTestInfo.Test)},
       (COALESCE(cntCore, 0)::NUMERIC / cntTest::NUMERIC) * 100 as {nameof(UserExecuteTestInfo.Percent)},
       cte.try_execute as {nameof(UserExecuteTestInfo.NumberAttempts)}
  FROM cte
LEFT OUTER JOIN cor on cor.test_id = cte.test_id and cor.try_execute = cte.try_execute
INNER JOIN core.test t on t.test_id = cte.test_id";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<UserExecuteTestInfo>(query, new {userId}))
                .ToArray());
        }

        /// <inheritdoc />
        public async Task<ActiveUserInfo[]> GetMostActivityUser()
        {
            const string query = @$"
SELECT u.user_id as {nameof(ActiveUserInfo.UserId)},
       login as {nameof(ActiveUserInfo.Login)},
       cnt as {nameof(ActiveUserInfo.NumberCompletedTasks)}
  FROM core.user u
            INNER JOIN (SELECT user_id, count(1) as cnt
            FROM core.test_result
                GROUP BY user_id
            LIMIT 3) t2 on u.user_id = t2.user_id
            ORDER BY cnt desc";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<ActiveUserInfo>(query))
                .ToArray());
        }
    }
}