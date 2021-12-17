using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Automated.Testing.System.IntegrationTests
{
    [TestClass]
    public class DictionaryControllerTests
    {
        private readonly TestHostFixture _testHostFixture = new();
        private HttpClient _httpClient;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void SetUp()
        {
            _httpClient = _testHostFixture.Client;
            _serviceProvider = _testHostFixture.ServiceProvider;
        }

        [TestMethod]
        public async Task ShouldReturnCorrectResponseForSuccessGetItems()
        {
            var request = new HttpRequestMessage() {
                RequestUri = new Uri("http://localhost:5000/api/Dictionary/GetAllDictionary"),
                Method = HttpMethod.Get,
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", await  GetLoginToken()) }
            };
            var response = await _httpClient.SendAsync(request);
            var dictionaryResponseContent = await response.Content.ReadAsStringAsync();
            var dictionaryResult = JsonConvert.DeserializeObject<ServiceResponse<DictionaryDto[]>>(dictionaryResponseContent);
            
            Assert.AreEqual(true, dictionaryResult.Content.Length > 0);
        }

        private async Task<string> GetLoginToken()
        {
            var credentials = new AuthenticateRequest
            {
                Username = "string",
                Password = "string"
            };
            var response = await _httpClient.PostAsync("/api/User/Authenticate",
                new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, MediaTypeNames.Application.Json));
            var loginResponseContent = await response.Content.ReadAsStringAsync();
            var loginResult = JsonConvert.DeserializeObject<ServiceResponse<AuthenticateInfo>>(loginResponseContent);

            return loginResult.Content.JwtToken;
        }
    }
}
