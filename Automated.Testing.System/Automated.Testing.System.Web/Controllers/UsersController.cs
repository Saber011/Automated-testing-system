using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.Core.Execute;
using Automated.Testing.System.Core.Execute.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Automated.Testing.System.Web.Controllers
{
    /// <summary>
    /// Api для работы с пользователями
    /// </summary>
    //  [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<ServiceResponse<UserDto[]>> GetAllUsers()
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _userService.GetAllUsersAsync());
        }
        
        /// <summary>
        /// Получить пользователя по id.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<UserDto> GetUserById(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }
        
        /// <summary>
        /// Получить удалить пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpDelete]
        public async  Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await _userService.RemoveUserAsync(id));
        }

        /// <summary>
        /// Обновление информации пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPut]
        [AllowAnonymous]
        public async Task<ServiceResponse<bool>> UpdateUserInfo(UpdaterUserRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await _userService.UpdateUserInfoAsync(request));
        }
    }
}