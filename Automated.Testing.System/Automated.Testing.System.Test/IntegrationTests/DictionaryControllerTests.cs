using System.Net.Http;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Test.Extensions;
using Automated.Testing.System.Test.Interfaces;
using Automated.Testing.System.Web.Controllers;
using FluentAssertions;
using Xbehave;
using Xunit;

namespace Automated.Testing.System.Test.IntegrationTests
{
    [Collection("Service controllers")]
    public class DictionaryControllerTests
    {
        private readonly ITestEnvironment _environment;

		public DictionaryControllerTests(ApplicationFactory factory)
		{
            _environment = factory.CreateEnvironment("/api/Dictionary/");
        }

        [Scenario(DisplayName = "Получение всех словарей.")]
        public void GetAllDictionary(HttpClient client, DictionaryDto[] dictionaries)
        {
            // Arrange
            "Создание `HTTP`-клиента с авторизацией"
                .x(() =>
                {
                    client = _environment
                        .CreateClient(options =>
                        {
                             options.AddDefaultAuthorization();
                        });
                });

            // Act
            "Обращение к методу `API` - `GetAllDictionary`"
                .x(async () =>
                {
                    using var response = await client.GetAsync(
                        nameof(DictionaryController.GetAllDictionary));

                    dictionaries = await response.ReadAsServiceResponseContentAsync<DictionaryDto[]>();
                });

            // Assert
            "Возвращаемый результат содержит элементы"
                .x(() => dictionaries.Should().NotBeEmpty());

            "Все идентификаторы в результате корректны и уникальны"
                .x(() =>
                {
                    dictionaries
                        .Should()
                        .NotContain(directionActivity => directionActivity.DictionaryId <= 0)
                        .And.OnlyHaveUniqueItems(directionActivity => directionActivity.DictionaryId);
                });

            "Нет пустых имен"
                .x(() =>
                {
                    dictionaries
                        .Should()
                        .NotContain(directionActivity => string.IsNullOrWhiteSpace(directionActivity.Name));
                });
        }
    }
}
