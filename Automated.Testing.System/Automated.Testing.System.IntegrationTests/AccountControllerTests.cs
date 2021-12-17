using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Automated.Testing.System.IntegrationTests
{
    [TestClass]
    public class AccountControllerTests
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
        public async Task ShouldExpectWhenLoginWithInvalidCredentials()
        {
            var credentials = new AuthenticateRequest
            {
                Username = "admin",
                Password = "invalidPassword"
            };
            var response = await _httpClient.PostAsync("/api/User/Authenticate",
                new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, MediaTypeNames.Application.Json));
            var loginResponseContent = await response.Content.ReadAsStringAsync();
            var loginResult = JsonConvert.DeserializeObject<ServiceResponse<AuthenticateInfo>>(loginResponseContent);
            
            Assert.AreEqual("Username or password is incorrect", loginResult.ResponseInfo.ErrorMessage);
        }

        [TestMethod]
        public async Task ShouldReturnCorrectResponseForSuccessLogin()
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
            
            Assert.AreEqual(credentials.Username, loginResult.Content.Login);
        }
    }
}
