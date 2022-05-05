using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.Core.Execute.models;
using Newtonsoft.Json;

namespace Automated.Testing.System.Test
{
    /// <summary>
    /// Вспомогательный класс для работы с <see cref="HttpClient"/>.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Перегрузка метода <see cref="WebRequestMethods.Http.HttpClientExtensions.PostAsJsonAsync{T}(System.HttpClient,T)"/>.
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
        
        public static HttpClient SetToken(this HttpClient client)
        {
            var credentials = new AuthenticateRequest
            {
                Username = "string@mail.ru",
                Password = "string"
            };
            var test = new StringContent(JsonConvert.SerializeObject(credentials),
                Encoding.UTF8, 
                "application/json");
            var response = client.PostAsync("/api/Account/Authenticate", test).Result;
            var loginResponseContent = response.Content.ReadAsStringAsync().Result;
            var loginResult = JsonConvert.DeserializeObject<ServiceResponse<AuthenticateInfo>>(loginResponseContent);
            client.DefaultRequestHeaders.Add(
                    HttpRequestHeader.Authorization.ToString(), 
                $"Bearer {loginResult.Content.JwtToken}");
            return client;
        }
    }
}