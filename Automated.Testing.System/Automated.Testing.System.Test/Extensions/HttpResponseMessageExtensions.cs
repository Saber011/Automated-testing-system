using System;
using System.Net.Http;
using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.Core.Execute.models;
using Newtonsoft.Json;

namespace Automated.Testing.System.Test.Extensions
{
    /// <summary>
    /// Вспомогательный класс для работы с <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static async Task<T> ReadAsServiceResponseContentAsync<T>(this HttpResponseMessage responseMessage)
        {
            Guard.NotNull(responseMessage, nameof(responseMessage));

            responseMessage.EnsureSuccessStatusCode();

            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<T>>(responseContent);

			return serviceResponse!.Content;
        }
    }
}