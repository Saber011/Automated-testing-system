using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Entities;
using Automated.Testing.System.DataAccess.Interfaces;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Dapper;

namespace Automated.Testing.System.DataAccess.Postgres.Repositories
{
    /// <inheritdoc />
    public class DictionaryRepository: IDictionaryRepository
    {
        private readonly IPostgresService _postgresService;

        /// <summary>
        /// Конструктор <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="postgresService">Сервис для работы с `Postgres`.</param>
        public DictionaryRepository(IPostgresService postgresService)
        {
            Guard.NotNull(postgresService, nameof(postgresService));

            _postgresService = postgresService;
        }

        /// <inheritdoc />
        public async Task<Dictionary[]> GetAllDictionaryAsync()
        {
            var query = $@"
SELECT dictionary_id AS {nameof(Dictionary.DictionaryId)},
       dictionary_name AS {nameof(Dictionary.Name)},
       table_name AS {nameof(Dictionary.TableName)}
  FROM core.dictionary";
            
            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<Dictionary>(query)).ToArray());
        }

        /// <inheritdoc />
        public async Task<string> GetDictionaryTableName(int id)
        {
            var query = $@"
SELECT table_name
  FROM core.dictionary
  WHERE dictionary_id = :id";

            return await _postgresService.Execute(query, async connection
                => await connection.QueryFirstOrDefaultAsync<string>(query, new { id }));

        }

        /// <inheritdoc />
        public async Task<DictionaryItem[]> GetDictionaryElementsByDictionaryIdAsync(int id)
        {
            var tableName = await GetDictionaryTableName(id);

            if (!string.IsNullOrWhiteSpace(tableName))
            {
                var query = $@"
SELECT *
  FROM core.{tableName}";
                var result = new List<DictionaryItem>();
                
                 return await _postgresService.Execute(query, async connection
                    =>
                 {
                     await using var command = connection.CreateCommand();
                     command.CommandText = query;
                     command.CommandType = CommandType.Text;

                     await using var reader = command.ExecuteReader();
                     while (await reader.ReadAsync())
                     {
                         result.Add(new DictionaryItem()
                         {
                             ElementId = reader.GetInt32(0),
                             Name = reader.GetString(1)
                         });
                     }

                     return result.ToArray();
                 });
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<bool> CreateDictionaryItemAsync(string tableName, string dictionaryElementName)
        {
            var tableColumn = await GetTableFiledNames(tableName);
            var query = $@"
INSERT INTO core.{tableName} ({tableColumn[1]}) 
     VALUES (:dictionaryElementName)";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("dictionaryElementName", dictionaryElementName);
                _ = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDictionaryItemAsync(string tableName, int dictionaryElementId, string dictionaryElementName)
        {
            var tableColumn = await GetTableFiledNames(tableName);
            var query = $@"
UPDATE core.{tableName}
   SET {tableColumn[1]} = :name
 WHERE {tableColumn[0]} = :id";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("id", dictionaryElementId);
                command.Parameters.AddWithValue("name", dictionaryElementName);
                _ = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> DeleteDictionaryItemAsync(string tableName, int dictionaryElementId)
        {
            var tableColumn = await GetTableFiledNames(tableName);
            var query = $@"
DELETE FROM core.{tableName}
 WHERE {tableColumn[0]} = :dictionaryElementId";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(query, new {dictionaryElementId}, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }
        
        private async Task<string[]> GetTableFiledNames(string tableName)
        {
            var query = $@"
SELECT column_name
  FROM information_schema.columns
 WHERE table_schema = 'core'
   AND table_name = :tableName";
            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<string>(query, new {tableName})).ToArray());
        }
    }
}