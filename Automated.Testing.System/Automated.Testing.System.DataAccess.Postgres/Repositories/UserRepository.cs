using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Postgres.Entities;
using Automated.Testing.System.DataAccess.Postgres.Extensions;
using Automated.Testing.System.DataAccess.Postgres.Repositories.Interfaces;
using Automated.Testing.System.DatabaseProvider.Postgres;
using Dapper;

namespace Automated.Testing.System.DataAccess.Postgres.Repositories
{
    /// <inheritdoc />
    public class UserRepository: IUserRepository
    {
        private readonly IPostgresService _postgresService;

        /// <summary>
        /// Конструктор <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="postgresService">Сервис для работы с `Postgres`.</param>
        public UserRepository(IPostgresService postgresService)
        {
            Guard.NotNull(postgresService, nameof(postgresService));

            _postgresService = postgresService;
        }

        /// <inheritdoc />
        public async Task<User[]> GetAllAsync()
        {
            var query = $@"
SELECT user_id AS {nameof(User.Id)},
       login AS {nameof(User.Login)},
       password AS {nameof(User.Password)}
  FROM core.user";
            
            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<User>(query)).ToArray());
        }

        /// <inheritdoc />
        public async Task<User> GetByIdAsync(int id)
        {
            var query = $@"
SELECT user_id AS {nameof(User.Id)},
       login AS {nameof(User.Login)},
       password AS {nameof(User.Password)}
  FROM core.""user""
 WHERE user_id = :id";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryFirstAsync<User>(query, new { id })));
        }
        
        /// <inheritdoc />
        public async Task<User> GetByLoginAsync(string login)
        {
            var query = $@"
SELECT user_id AS {nameof(User.Id)},
       login AS {nameof(User.Login)},
       password AS {nameof(User.Password)}
  FROM core.""user""
 WHERE login like :login";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryFirstOrDefaultAsync<User>(query, new { login })));
        }

        /// <inheritdoc />
        public async Task<bool> CreateUserAsync(string login, string password, RefreshToken refreshToken)
        {
            const string query = @"
INSERT INTO core.user (login, password)
     VALUES (:login, :password)
     RETURNING user_id;";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("login", login);
                command.Parameters.AddWithValue("password", password);
                var userId = await command.ExecuteScalarAsync();

                await transaction.CommitAsync();
                await CreateUserToken(refreshToken, (int)userId);

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> RemoveUserAsync(int id)
        {
            var query = @"
DELETE FROM core.""user""
 WHERE user_id = :id";
            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await connection.ExecuteAsync(query, new { id }, transaction);

                await transaction.CommitAsync();

                return true;
            });
        }
        
        /// <inheritdoc />
        public async Task<bool> UpdateUserInfoAsync(int id, string login, string password)
        {
            const string query = @"
UPDATE core.""user"" 
   SET login = :name,
       password = :password
 WHERE user_id = :id";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("login", login);
                command.Parameters.AddWithValue("password", password);
                _ = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<RefreshToken[]> GetUserTokens(int id)
        {
            var query = $@"
SELECT token,
       expires,
       created,
       created_by_ip,
       revoked,
       revoked_by_ip,
       replaced_by_token
  FROM core.user_token
 WHERE user_id = :id";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<RefreshToken>(query, new { id })).ToArray());
        }

        /// <inheritdoc />
        public async Task<int?> GetUseridByTokens(string token)
        {
            var query = $@"
SELECT user_id
  FROM core.user_token
 WHERE token = :token";

            return await _postgresService.Execute(query, async connection
                => (await connection.QueryAsync<int>(query, new { token })).FirstOrDefault());
        }

        /// <inheritdoc />
        public async Task<bool> UpdateUserToken(RefreshToken token)
        {
            const string query = @"
UPDATE core.user_token
   SET token = :token,
       expires = :expires,
       created = :created,
       created_by_ip = :created_by_ip,
       revoked = :revoked,
       revoked_by_ip = :revoked_by_ip,
       replaced_by_token = :replaced_by_token
 WHERE token = :token";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("token", token.Token);
                command.Parameters.AddWithValue("expires", token.Expires);
                command.Parameters.AddWithValue("created", token.Created);
                command.Parameters.AddWithNullValue("created_by_ip", token.CreatedByIp);
                command.Parameters.AddWithNullValue("revoked", token.Revoked);
                command.Parameters.AddWithNullValue("revoked_by_ip", token.RevokedByIp);
                command.Parameters.AddWithNullValue("replaced_by_token", token.ReplacedByToken);
                
                _ = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }

        /// <inheritdoc />
        public async Task<bool> CreateUserToken(RefreshToken token, int id)
        {
            const string query = @"
INSERT INTO core.user_token (user_id, token, expires, created, created_by_ip, revoked, revoked_by_ip, replaced_by_token)
     VALUES (:user_id, :token, :expires, :created, :created_by_ip, :revoked, :revoked_by_ip, :replaced_by_token)";

            return await _postgresService.Execute(query, async connection =>
            {
                await using var transaction = await connection.BeginTransactionAsync();
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                
                command.Parameters.AddWithValue("user_id", id);
                command.Parameters.AddWithValue("token", token.Token);
                command.Parameters.AddWithValue("expires", token.Expires);
                command.Parameters.AddWithValue("created", token.Created);
                command.Parameters.AddWithValue("created_by_ip", token.CreatedByIp);
                command.Parameters.AddWithNullValue("revoked", token.Revoked);
                command.Parameters.AddWithNullValue("revoked_by_ip", token.RevokedByIp);
                command.Parameters.AddWithNullValue("replaced_by_token", token.ReplacedByToken);

                _ = await command.ExecuteNonQueryAsync();

                await transaction.CommitAsync();

                return true;
            });
        }
    }
}