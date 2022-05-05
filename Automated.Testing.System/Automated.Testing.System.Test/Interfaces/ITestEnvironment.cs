using System.Net.Http;

namespace Automated.Testing.System.Test.Interfaces
{
    /// <summary>
    /// Тестовое окружение.
    /// </summary>
    public interface ITestEnvironment
    {
        /// <summary>
        /// Создаёт `HTTP`-клиент.
        /// </summary>
        HttpClient CreateClient();
    }
}