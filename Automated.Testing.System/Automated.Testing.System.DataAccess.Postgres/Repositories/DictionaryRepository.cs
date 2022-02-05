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
using Microsoft.VisualBasic;

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
        public async Task<string> GetDictionaryTableNameAsync(int id)
        {
            var query = $@"
SELECT table_name
  FROM core.dictionary
  WHERE dictionary_id = :id";

            return await _postgresService.Execute(query, async connection
                => await connection.QueryFirstOrDefaultAsync<string>(query, new { id }));

        }

        /// <inheritdoc />
        public async Task<Article[]> GetArticlesAsync(string? title, int pageSize, int pageNumber, int[]? categoryIds)
        {
            var titleFilter = !string.IsNullOrWhiteSpace(title)
                ? $"AND t.title like '%{title}%'"
                : string.Empty;
            var catFilter = categoryIds?.Length > 0
                ? $"inner join core.ref_article_category ref on ref.article_id = t.article_id and  ref.category_id in ({string.Join(",", categoryIds)})"
                : string.Empty;
            
            var startRow = (pageNumber - 1) * pageSize + 1;
            var endRow = startRow + pageSize - 1;
            
            var query = @$"
SELECT distinct res.article_id as {nameof(Article.ArticleId)},
       text as {nameof(Article.Text)},
       title as {nameof(Article.Title)},
       row_number,
       total as {nameof(Article.Total)}
FROM (SELECT article_id,
       text,
       title,
       row_number,
       COUNT (article_id) OVER () as total
  FROM (SELECT t.article_id,
               t.text,
               t.title,
               ROW_NUMBER () OVER (
                   ORDER BY t.title
                )
          FROM core.article t
          {catFilter}
         WHERE 1 = 1
           {titleFilter}) as dta) as res
WHERE row_number BETWEEN :startRow and :endRow";
            
            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<Article>(query, new { startRow, endRow })).ToArray());
        }

        /// <inheritdoc />
        public async Task<(int, int)[]> GetArticleCategoriesAsync(int[] articleIds)
        {
            var query = @$"
SELECT article_id,
       category_id
  FROM core.ref_article_category
 WHERE article_id in ({string.Join(",", articleIds)})";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<(int, int)>(query)).ToArray());
        }

        /// <inheritdoc />
        public async Task<bool> CreateArticleAsync(string title, string text, int[] categoryIds)
        {
            StringBuilder categoryInsertQuery = new();
            if (categoryIds.Length > 0)
            {
                foreach (var categoryId in categoryIds)
                {
                    categoryInsertQuery.Append($"INSERT INTO core.ref_article_category (article_id, category_id) VALUES (:articleId, {categoryId});");
                }
            }
            const string query = @"
INSERT INTO core.article (text, title)
     VALUES (:text, :title)
     RETURNING article_id;";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                var articleId = (int) await connection.ExecuteScalarAsync(query, new { title, text }, transaction);
                
                await connection.ExecuteAsync(categoryInsertQuery.ToString(), new { articleId }, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> UpdateArticleAsync(int articleId, string title, string text, int[] categoryIds)
        {
            StringBuilder categoryUpdateQuery = new();
            if (categoryIds.Length > 0)
            {
                categoryUpdateQuery.Append("DELETE FROM core.ref_article_category WHERE article_id = :articleId;");
                foreach (var categoryId in categoryIds)
                {
                    categoryUpdateQuery.Append($"INSERT INTO core.ref_article_category (article_id, category_id) VALUES (:articleId, {categoryId});");
                }
            }
            
            const string query = @"
UPDATE core.article
   SET text = :text,
       title = :title
 WHERE article_id = :articleId";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(categoryUpdateQuery.ToString(), new { articleId }, transaction);
                await connection.ExecuteAsync(query, new { articleId, title, text }, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> DeleteArticleAsync(int articleId)
        {
            const string? query = $@"
DELETE FROM core.ref_article_category
 WHERE article_id = :articleId;
DELETE FROM core.article
 WHERE article_id = :articleId;";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(query, new {articleId}, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<DictionaryItem[]> GetDictionaryElementsByDictionaryIdAsync(int id)
        {
            var tableName = await GetDictionaryTableNameAsync(id);

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

            return Array.Empty<DictionaryItem>();
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