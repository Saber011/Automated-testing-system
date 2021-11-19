using System;
using System.Threading.Tasks;
using Npgsql;

namespace Automated.Testing.System.DatabaseProvider.Postgres
{
    public interface IPostgresService
    {
         /// <summary>
    /// Выполняет указанный запрос и возвращает результат выполнения.
    /// </summary>
    /// <param name="query">Текст запроса.</param>
    /// <param name="execute">Функция выполнения запроса.</param>
         Task<T> Execute<T>(
      string query,
      Func<NpgsqlConnection, Task<T>> execute);

    /// <summary>Выполняет команду возвращает результат выполнения.</summary>
    /// <typeparam name="T">Тип результата запроса.</typeparam>
    /// <param name="command">Одна или несколько DML команд</param>
    /// <param name="executeCommand">Функция выполнения команды</param>
    Task<T> ExecuteCommand<T>(
      NpgsqlCommand command,
      Func<NpgsqlCommand, Task<T>> executeCommand);
    }
}