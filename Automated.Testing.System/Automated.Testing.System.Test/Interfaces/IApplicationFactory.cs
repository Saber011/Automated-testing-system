using System;

namespace Automated.Testing.System.Test.Interfaces
{
    /// <summary>
    /// Фабрика для начальной инициализации приложения.
    /// </summary>
    public interface IApplicationFactory
    {
        /// <summary>
        /// Создает тестовое окружение.
        /// </summary>
        ITestEnvironment CreateEnvironment(string controllerPath);
    }
}