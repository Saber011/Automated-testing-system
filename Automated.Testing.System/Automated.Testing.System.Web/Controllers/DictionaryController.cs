using System.Threading.Tasks;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.Dictionary.Dto.Request;
using Automated.Testing.System.Core.Execute;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automated.Testing.System.Web.Controllers
{
    /// <summary>
    /// Api для работы со справочниками
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        
        /// <summary>
        /// Получить все справочники
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<ServiceResponse<DictionaryDto[]>> GetAllDictionary()
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.GetAllAsync());
        }

        /// <summary>
        /// Получить элементы словаря по справочнику
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<ServiceResponse<DictionaryItemDto[]>> GetDictionaryElementsByDictionaryId(int id)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.GetByIdAsync(id));
        }
        
        /// <summary>
        /// Создать новый элемент
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<bool>> CreateDictionaryItem(CreateDictionaryElementRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.CreateDictionaryItemAsync(request));
        }
        
        /// <summary>
        /// Обновить данные элемента словаря
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPut]
        public async Task<ServiceResponse<bool>> UpdateDictionaryItem(UpdateDictionaryElementRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.UpdateDictionaryItemAsync(request));
        }
        
        /// <summary>
        /// Удалить элемент словаря.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpDelete]
        public async Task<ServiceResponse<bool>> DeleteDictionaryItem(DeleteDictionaryElementRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.DeleteDictionaryItemAsync(request));
        }
        
        /// <summary>
        /// Получить статьи
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<ServiceResponse<ArticleDto[]>> GetArticles([FromQuery]GetArticlesRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.GetArticlesAsync(request));
        }
        
        /// <summary>
        /// Cоздать статью
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<bool>> CreateArticle(CreateArticleRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.CreateArticleAsync(request));
        }
        
        /// <summary>
        /// Обновить статью
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPut]
        public async Task<ServiceResponse<bool>> UpdateArticle(UpdateArticleRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.UpdateArticleAsync(request));
        }
        
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpDelete]
        public async Task<ServiceResponse<bool>> DeleteArticle(int articleId)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _dictionaryService.DeleteArticleAsync(articleId));
        }
    }
}