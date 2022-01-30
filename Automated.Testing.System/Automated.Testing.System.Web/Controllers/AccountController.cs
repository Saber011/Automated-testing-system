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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ServiceResponse<AuthenticateInfo>> Authenticate(AuthenticateRequest request)
        {
            var response = await _accountService.AuthenticateAsync(request, IpAddress());

            SetTokenCookie(response.RefreshToken);

            return ServiceResponseHelper.ConvertToServiceResponse(response);
        }

        /// <summary>
        /// Получить рефреш токен
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ServiceResponse<AuthenticateInfo>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshTokenAsync(refreshToken, IpAddress());
            
            SetTokenCookie(response.RefreshToken);

            return ServiceResponseHelper.ConvertToServiceResponse(response);
        }

        /// <summary>
        /// Удалить токен.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        public async Task<ServiceResponse<bool>> RevokeToken([FromBody] RevokeTokenRequest? model)
        {
            var token = model?.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new  ValidationException("Token is required");

            var response = await _accountService.RevokeTokenAsync(token, IpAddress());

            return ServiceResponseHelper.ConvertToServiceResponse(response);
        }
        
        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<bool>> RegisterUser(RegisterUserRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await _accountService.RegisterUserAsync(request, IpAddress()));
        }

        /// <summary>
        /// Получить рефреш токены
        /// </summary>
        /// <response code = "200" > Успешное выполнение.</response>
        /// <response code = "401" > Данный запрос требует аутентификации.</response>
        /// <response code = "500" > Непредвиденная ошибка сервера.</response>
        [HttpGet]
        public async Task<ServiceResponse<RefreshToken[]>> GetRefreshTokens(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return ServiceResponseHelper.ConvertToServiceResponse(user.RefreshTokens);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress != null
                ? HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() 
                : string.Empty;
        }
    }
}