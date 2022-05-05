using System;
using Automated.Testing.System.Core.Core;

namespace Automated.Testing.System.Test.Extensions
{
    /// <summary>
    /// Вспомогательный класс для работы с <see cref="HttpClientOptions"/>.
    /// </summary>
    public static class HttpClientOptionsExtensions
    {
        /// <summary>
        /// Устанавливает токен авторизации по умолчанию.
        /// </summary>
        public static HttpClientOptions AddDefaultAuthorization(this HttpClientOptions options)
        {
            Guard.NotNull(options, nameof(options));

            options.AuthorizationToken = "";

            return options;
        }
    }
}