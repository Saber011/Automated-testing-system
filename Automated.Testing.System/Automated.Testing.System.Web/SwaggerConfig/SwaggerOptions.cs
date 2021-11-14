
namespace Automated.Testing.System.Web.SwaggerConfig
{
    /// <summary>
    /// Настройки swagger.
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// Имя хоста.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Физический путь к папке приложения.
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Имена файлов с xml описанием структур
        /// и интерфейсов для генерации OpenAPI документа.
        /// </summary>
        public string[] FileNames { get; set; }

        /// <summary>
        /// Виртуальная директория приложения на веб-сервере.
        /// </summary>
        public string ApplicationVirtualPath { get; set; }
    }
}