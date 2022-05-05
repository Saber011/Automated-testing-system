namespace Automated.Testing.System.Test
{
    /// <summary>
    /// Настройки `HTTP`-клиента.
    /// </summary>
    public sealed class HttpClientOptions
    {
        /// <summary>
        /// Токен авторизации.
        /// </summary>
        public string? AuthorizationToken { get; set; }
    }
}