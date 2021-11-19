using System;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Automated.Testing.System.DatabaseProvider.Postgres
{
    public class PostgresService : IPostgresService
    {
        private readonly PostgresConfig _config;
        /// <summary>
        /// Конструктор <see cref="PostgresService"/>.
        /// </summary>
        public PostgresService(IOptions<PostgresConfig> config)
        {
            _config = config.Value;
        }
        
         /// <inheritdoc />
        public async Task<T> Execute<T>(
            string query,
            Func<NpgsqlConnection, Task<T>> execute)
        {
            Guard.NotNullOrWhiteSpace(query, nameof(query));
            Guard.NotNull(execute, nameof(execute));

            await using var connection = new NpgsqlConnection(_config.ConnectionString);

            await connection
                .OpenAsync();

            var result = await execute(connection);
                return result;
            }

        /// <inheritdoc />
        public async Task<T> ExecuteCommand<T>(NpgsqlCommand command, Func<NpgsqlCommand, Task<T>> executeCommand)
        {
            Guard.NotNull(command, nameof(command));
            Guard.NotNullOrWhiteSpace(command.CommandText, nameof(command.CommandText));
            Guard.NotNull(executeCommand, nameof(executeCommand));

            await using var connection = new NpgsqlConnection(_config.ConnectionString);

            await connection
                .OpenAsync();
            
                command.Connection = connection;

                var result = await executeCommand(command);

                return result;
        }
    }
}