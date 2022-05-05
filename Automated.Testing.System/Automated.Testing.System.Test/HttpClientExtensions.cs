using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;

namespace Automated.Testing.System.Test
{
    /// <summary>
    /// Вспомогательный класс для работы с <see cref="HttpClient"/>.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Перегрузка метода <see cref="System.Net.Http.HttpClientExtensions.PostAsJsonAsync{T}(System.Net.Http.HttpClient,string,T)"/>.
        /// Аналогичен вызову,
        /// <code>
        /// client.PostAsJsonAsync(requestUri, (object?)null);
        /// </code>
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string requestUri)
        {
            Guard.NotNull(client, nameof(client));
            Guard.NotNullOrWhiteSpace(requestUri, nameof(requestUri));

            return await client.PostAsJsonAsync(requestUri, (object?)null);
        }

        /// <summary>
        /// Выполняет `POST` запрос методов `API` передавая контент в формате `application/x-www-form-urlencoded`.
        /// </summary>
        public static async Task<HttpResponseMessage> PostAsFormDataAsync<T>(
            this HttpClient client,
            string requestUri,
            T request)
        {
            Guard.NotNull(client, nameof(client));
            Guard.NotNullOrWhiteSpace(requestUri, nameof(requestUri));
            Guard.NotNull(request, nameof(request));

            var content = new FormUrlEncodedContent(request.ToKeyValues());
            return await client.PostAsync(requestUri, content);
        }
    }
}