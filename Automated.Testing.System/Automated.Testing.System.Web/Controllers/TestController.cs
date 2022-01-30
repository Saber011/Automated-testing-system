using System.Threading.Tasks;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.Test.Dto;
using Automated.Testing.System.Common.Test.Dto.Request;
using Automated.Testing.System.Core.Execute;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.AspNetCore.Mvc;

namespace Automated.Testing.System.Web.Controllers
{
    /// <summary>
    /// Api для работы со справочниками
    /// </summary>
    // [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        
        /// <summary>
        /// Получить все тесты
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<TestDto[]>> GetTests(int[] categoryIds)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _testService.GetTestsAsync(categoryIds));
        }
        
        /// <summary>
        /// Получить все задачи теста
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<ServiceResponse<TestTaskDto[]>> GetTestTask(int testId)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _testService.GetTestTaskAsync(testId));
        }
        
        /// <summary>
        /// Проверить решение теста
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<TestTaskDto[]>> CheckTest(int testId)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _testService.GetTestTaskAsync(testId));
        }
        
        /// <summary>
        /// Добавить тест
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<bool>> AddTest(CreateTestRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _testService.CreateTestAsync(request));
        }
        
        /// <summary>
        /// Удалить тест
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<bool>> RemoveTest(int testId)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _testService.RemoveTestAsync(testId));
        }
        
        /// <summary>
        /// Обновить тест
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<bool>> UpdateTest(UpdateTestRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _testService.UpdateTestAsync(request));
        }

    }
}