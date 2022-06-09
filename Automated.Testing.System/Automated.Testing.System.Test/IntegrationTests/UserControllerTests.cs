using System.Net.Http;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Test.Extensions;
using Automated.Testing.System.Test.Interfaces;
using Automated.Testing.System.Web.Controllers;
using FluentAssertions;
using Xbehave;
using Xunit;

namespace Automated.Testing.System.Test.IntegrationTests
{
    [Collection("Service controllers")]
    public class UserControllerTests
    {
        private readonly ITestEnvironment _environment;

		public UserControllerTests(ApplicationFactory factory)
		{
            _environment = factory.CreateEnvironment("/api/User/");
        }

        [Scenario(DisplayName = "Получение всех пользователей.")]
        public void GetAllDictionary(HttpClient client, UserDto[] users)
        {
            // Arrange
            "Создание `HTTP`-клиента с авторизацией"
                .x(() =>
                {
                    client = _environment
                        .CreateClient()
                        .SetToken();
                });

            // Act
            "Обращение к методу `API` - `GetAllUsers`"
                .x(async () =>
                {
                    using var response = await client.GetAsync(
                        nameof(UserController.GetAllUsers));

                    users = await response.ReadAsServiceResponseContentAsync<UserDto[]>();
                });

            // Assert
            "Возвращаемый результат содержит элементы"
                .x(() => users.Should().NotBeEmpty());

            "Все идентификаторы в результате корректны и уникальны"
                .x(() =>
                {
                    users
                        .Should()
                        .NotContain(user => user.Id <= 0)
                        .And.OnlyHaveUniqueItems(user => user.Id);
                });

            "Нет пустых логинов"
                .x(() =>
                {
                    users
                        .Should()
                        .NotContain(user => string.IsNullOrWhiteSpace(user.Login));
                });
            
            "У всех пользователей есть роли"
                .x(() =>
                {
                    users
                        .Should()
                        .NotContainNulls(user => user.Roles);
                });
        }
        
        
        [Scenario(DisplayName = "Получение информации о пользователе.")]
        [MemberData(nameof(GetUserIdData))]
        public void GetUserById(int userId, HttpClient client, UserDto user)
        {
            // Arrange
            "Создание `HTTP`-клиента с авторизацией"
                .x(() =>
                {
                    client = _environment
                        .CreateClient()
                        .SetToken();
                });

            // Act
            "Обращение к методу `API` - `GetUserById`"
                .x( async () =>
                {
                    using var response = await client.GetAsync(
                        nameof(UserController.GetUserById) + $"?id={userId}");

                    user = await response.ReadAsServiceResponseContentAsync<UserDto>();
                });

            // Assert
            "Возвращаемый результат содержит элемент"
                .x(() => user.Should().NotBeNull());

            "Содержит логин"
                .x(() =>
                {
                    user
                        .Should()
                        .NotBe(string.IsNullOrWhiteSpace(user.Login));
                });
            
            "Содержит роли пользователя"
                .x(() =>
                {
                    user
                        .Should()
                        .NotBe(user.Roles.Length == 0);
                });
        }

        public static TheoryData<int> GetUserIdData = new()
        {
            1,
            3,
        };
    }
}
