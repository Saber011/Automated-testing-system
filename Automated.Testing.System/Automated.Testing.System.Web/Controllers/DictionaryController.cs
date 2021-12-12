using System.Threading.Tasks;
using Automated.Testing.System.Core.Execute;
using Automated.Testing.System.Core.Execute.models;
using Automated.Testing.System.UseCases.Handlers.Dictionary.Dto;
using Automated.Testing.System.UseCases.Handlers.Dictionary.Queries.GetAllDictionary;
using MediatR;
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
    public class DictionaryController : Controller
    {
        private readonly IMediator _mediator;

        public DictionaryController(IMediator mediator)
        {
            _mediator = mediator;
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
            return ServiceResponseHelper.ConvertToServiceResponse(await _mediator.Send(new GetAllDictionaryRequest()));
        }
    }
}