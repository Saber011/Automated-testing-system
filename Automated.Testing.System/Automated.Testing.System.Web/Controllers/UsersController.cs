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
    [Authorize]
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ServiceResponse<AuthenticateInfo>> Authenticate(AuthenticateRequest request)
        {
            var response = await _userService.AuthenticateAsync(request, IpAddress());

            SetTokenCookie(response.RefreshToken);

            return ServiceResponseHelper.ConvertToServiceResponse(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ServiceResponse<AuthenticateInfo>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken, IpAddress());
            
            SetTokenCookie(response.RefreshToken);

            return ServiceResponseHelper.ConvertToServiceResponse(response);
        }

        [HttpPost]
        public async Task<ServiceResponse<bool>> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new  ValidationException("Token is required");

            var response = await _userService.RevokeTokenAsync(token, IpAddress());

            return ServiceResponseHelper.ConvertToServiceResponse(response);
        }

        [HttpGet]
        public async Task<ServiceResponse<UserDto[]>> GetAllUsers()
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await  _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }
        
        [HttpDelete]
        public async  Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await _userService.RemoveUserAsync(id));
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ServiceResponse<bool>> RegisterUser(RegisterUserRequest request)
        {
            return ServiceResponseHelper.ConvertToServiceResponse(await _userService.CreateUserAsync(request, IpAddress()));
        }
        
        [HttpGet("{id}/refresh-tokens")]
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