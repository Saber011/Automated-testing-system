using System;
using System.Net.Http;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.Dictionary.Dto.Request;
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
                        .CreateClient()
                        .SetToken();
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
        
        
        [Scenario(DisplayName = "Получение элементов словаря по id.")]
        [MemberData(nameof(GetDictionaryElementsByDictionaryIdData))]
        public void GetDictionaryElementsByDictionaryId(int dictId, HttpClient client, DictionaryItemDto[] dictionaryItems)
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
            "Обращение к методу `API` - `GetDictionaryElementsByDictionaryId`"
                .x( async () =>
                {
                    using var response = await client.GetAsync(
                        nameof(DictionaryController.GetDictionaryElementsByDictionaryId) + $"?id={dictId}");

                    dictionaryItems = await response.ReadAsServiceResponseContentAsync<DictionaryItemDto[]>();
                });

            // Assert
            "Возвращаемый результат содержит элементы"
                .x(() => dictionaryItems.Should().NotBeEmpty());

            "Все идентификаторы в результате корректны и уникальны"
                .x(() =>
                {
                    dictionaryItems
                        .Should()
                        .NotContain(directionActivity => directionActivity.ElementId <= 0)
                        .And.OnlyHaveUniqueItems(directionActivity => directionActivity.ElementId);
                });

            "Нет пустых имен"
                .x(() =>
                {
                    dictionaryItems
                        .Should()
                        .NotContain(directionActivity => string.IsNullOrWhiteSpace(directionActivity.Name));
                });
        }
        
        [Scenario(DisplayName = "Получение статей.")]
        [MemberData(nameof(GetArticleData))]
        public void GetArticles(GetArticlesRequest request, HttpClient client, ArticleDto[] dictionaryItems)
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
            "Обращение к методу `API` - `GetArticles`"
                .x( async () =>
                {
                    var requestUri = @$"{nameof(DictionaryController.GetArticles)}?{nameof(request.Title)}={request.Title}&{nameof(request.CategoryIds)}={string.Join(",", request.CategoryIds ?? Array.Empty<int>())}&{nameof(request.PageSize)}={request.PageSize}&{nameof(request.PageNumber)}={request.PageNumber}";
                    using var response = await client.GetAsync(requestUri);

                    dictionaryItems = await response.ReadAsServiceResponseContentAsync<ArticleDto[]>();
                });

            // Assert
            "Возвращаемый результат содержит элементы"
                .x(() => dictionaryItems.Should().NotBeEmpty());

            "Все идентификаторы в результате корректны и уникальны"
                .x(() =>
                {
                    dictionaryItems
                        .Should()
                        .NotContain(article => article.ArticleId <= 0)
                        .And.OnlyHaveUniqueItems(directionActivity => directionActivity.ArticleId);
                });

            "Статья содержит текст"
                .x(() =>
                {
                    dictionaryItems
                        .Should()
                        .NotContain(article => string.IsNullOrWhiteSpace(article.Text));
                });
            
            "Статья содержит заголовок"
                .x(() =>
                {
                    dictionaryItems
                        .Should()
                        .NotContain(article => string.IsNullOrWhiteSpace(article.Title));
                });
        }
        
        public static TheoryData<GetArticlesRequest> GetArticleData = new()
        {
            new GetArticlesRequest
            {
                CategoryIds = new [] { 1 },
                PageNumber = 1,
                PageSize = 5,
            },
        };

        public static TheoryData<int> GetDictionaryElementsByDictionaryIdData = new()
        {
            1,
            2,
            3,
        };
    }
}
