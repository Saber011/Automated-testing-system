using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Entities;
using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Dapper;

namespace Automated.Testing.System.DataAccess.Postgres.Repositories
{
    /// <inheritdoc />
    public class TestRepository: ITestRepository
    {
        private readonly IPostgresService _postgresService;

        /// <summary>
        /// Конструктор <see cref="TestRepository"/>.
        /// </summary>
        /// <param name="postgresService">Сервис для работы с `Postgres`.</param>
        public TestRepository(IPostgresService postgresService)
        {
            Guard.NotNull(postgresService, nameof(postgresService));

            _postgresService = postgresService;
        }

        /// <inheritdoc />
        public async Task<TestTask[]> GetTestTaskAsync(int testId)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            
            const string query = $@"
select test_task_id AS {nameof(TestTask.TestTaskId)},
       test_id AS {nameof(TestTask.TestId)},
       description AS {nameof(TestTask.Description)},
       test_type_id AS {nameof(TestTask.TypeId)}
from core.test_task
where test_id = :testId";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<TestTask>(query, new { testId})).ToArray());
        }
        
        /// <inheritdoc />
        public async Task<(int, int, string)[]> GetTestTaskResponseOptionAsync(int[] taskIds)
        {
            Guard.NotNullOrEmpty(taskIds, nameof(taskIds));
            
             var query = $@"
select response_option,
       test_task_id,
       test_task_response_option_id
from core.test_task_response_option
where test_task_id in ({string.Join(",", taskIds)})";

            var result = new List<(int, int, string)>();
            return await _postgresService.Execute(query, async connection
                =>
            {
                await using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                await using var reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    result.Add((Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), reader[0].ToString()));
                }

                return result.ToArray();
            });
        }

        /// <inheritdoc />
        public async Task<Test[]> GetTestsAsync(int[] categoryIds)
        {
            var filter = string.Empty;
            if (categoryIds.Length > 0)
            {
                filter = $"WHERE category_id in ({string.Join(",", categoryIds)})";
            }
            
            var query = $@"
select distinct t.test_id AS {nameof(Test.TestId)},
       name AS {nameof(Test.Name)},
       0 AS {nameof(Test.CategoryId)}
from core.test t
inner join core.ref_test_category rtc on t.test_id = rtc.test_id
{filter}";
            
            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<Test>(query)).ToArray());
        }

        /// <inheritdoc />
        public Task<dynamic> CheckTestResultsAsync(dynamic request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<int> CreateTestAsync(string name, int userId)
        {
            Guard.NotNullOrWhiteSpace(name, nameof(name));
            
            const string query = $@"
INSERT INTO core.test (test_id, name, user_id)
VALUES (DEFAULT, :test, :userId)
RETURNING test_id;";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("test", name);
                command.Parameters.AddWithValue("userId", userId);
                var testId = await command.ExecuteScalarAsync();

                await transaction.CommitAsync();

                return (int)testId;
            });
        }

        /// <inheritdoc />
        public async Task<bool> CreateTestCategoryAsync(int testId, int[] categoryIds)
        {
            StringBuilder categoryInsertQuery = new();
            if (categoryIds.Length > 0)
            {
                foreach (var categoryId in categoryIds)
                {
                    categoryInsertQuery.Append($"INSERT INTO core.ref_test_category (test_id, category_id) VALUES ({testId}, {categoryId});");
                }
            }
            
            return await _postgresService.Execute(categoryInsertQuery.ToString(), async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = categoryInsertQuery.ToString();
                command.CommandType = CommandType.Text;
                
                await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<int> CreateTestTaskAsync(string taskName, int testId, int testType)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            Guard.GreaterThanZero(testType, nameof(testType));
            Guard.NotNullOrWhiteSpace(taskName, nameof(taskName));
            
            const string query = $@"
INSERT INTO core.test_task (test_task_id, description, test_id, test_type_id)
VALUES (DEFAULT, :test, :testId, :testType)
RETURNING test_task_id;";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("test", taskName);
                command.Parameters.AddWithValue("testId", testId);
                command.Parameters.AddWithValue("testType", testType);
                var testTaskId = await command.ExecuteScalarAsync();

                await transaction.CommitAsync();

                return (int)testTaskId;
            });
        }

        /// <inheritdoc />
        public  async  Task<bool> CreateTestTaskResponseOptionAsync(string[] options, int testTaskId)
        {
            Guard.GreaterThanZero(testTaskId, nameof(testTaskId));
            
            foreach (var option in options)
            {
                const string query = $@"
INSERT INTO core.test_task_response_option (test_task_response_option_id, response_option, test_task_id)
VALUES (DEFAULT, :option, :testTaskId)";

                await _postgresService.Execute(query, async connection =>
                {
                    await using var transaction = await connection.BeginTransactionAsync();
                    await using var command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                
                    command.Parameters.AddWithValue("option", option);
                    command.Parameters.AddWithValue("testTaskId", testTaskId);
                    _ = await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();

                    return true;
                });
            }

            return true;
        }
        
        /// <inheritdoc />
        public  async  Task<bool> CreateTestTaskAnswersAsync(string[] answers, int testTaskId)
        {
            Guard.GreaterThanZero(testTaskId, nameof(testTaskId));
            
            foreach (var answer in answers)
            {
                const string query = $@"
INSERT INTO core.test_task_answer (test_task_answer_id, test_task_id, answer)
VALUES (DEFAULT, :testTaskId, :answer)";

                await _postgresService.Execute(query, async connection =>
                {
                    await using var transaction = await connection.BeginTransactionAsync();
                    await using var command = connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                
                    command.Parameters.AddWithValue("answer", answer);
                    command.Parameters.AddWithValue("testTaskId", testTaskId);
                    _ = await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();

                    return true;
                });
            }
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveTestAsync(int testId)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            
            var testTaskIds = (await GetTestTaskAsync(testId)).Select(x => x.TestTaskId).ToArray();

            var query = $@"
DELETE FROM core.test_task_response_option
 WHERE test_task_id in ({string.Join(",", testTaskIds)});
DELETE FROM core.test_task_answer
 WHERE test_task_id in ({string.Join(",", testTaskIds)});
DELETE FROM core.test_task
 WHERE test_task_id in ({string.Join(",", testTaskIds)});
DELETE FROM core.ref_test_category
 WHERE test_id in ({string.Join(",", testTaskIds)});
DELETE FROM core.test
 WHERE test_id = :testId;";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(query, new {testId}, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public  async  Task<bool> UpdateTestAsync(int testId, string name, int categoryId)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            Guard.GreaterThanZero(categoryId, nameof(categoryId));
            Guard.NotNullOrWhiteSpace(name, nameof(name));
            
            const string query = $@"
UPDATE core.test
SET name = :test,
    category_id = :categoryId
WHERE test_id = :testId;";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("test", name);
                command.Parameters.AddWithValue("testId", testId);
                command.Parameters.AddWithValue("categoryId", categoryId);
                await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> DeleteTestInformationTaskAsync(int testId)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            
            var testTaskIds = (await GetTestTaskAsync(testId)).Select(x => x.TestTaskId).ToArray();

            var query = $@"
DELETE FROM core.test_task_response_option
 WHERE test_task_id in ({string.Join(",", testTaskIds)});
DELETE FROM core.test_task_answer
 WHERE test_task_id in ({string.Join(",", testTaskIds)});
DELETE FROM core.test_task
 WHERE test_task_id in ({string.Join(",", testTaskIds)});";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(query, new {testId}, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }

        private async Task<bool> RemoveTaskOptions(int taskId, string tableName)
        {
            Guard.GreaterThanZero(taskId, nameof(taskId));
            Guard.NotNullOrWhiteSpace(tableName, nameof(tableName));
            
            var testTaskIds = (await GetTestTaskAsync(taskId)).Select(x => x.TestTaskId).ToArray();

            var query = $@"
DELETE FROM core.{tableName}
 WHERE test_id in ({string.Join(",", testTaskIds)})";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(query,  transaction);

                await transaction.CommitAsync();

                return true;
            });
        }
    }
}