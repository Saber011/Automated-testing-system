using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Entities;
using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        
        /// <inheritdoc />
        public async Task<User> GetCurrentUserInfo()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (token is not null)
            {
                return await _userRepository.GetUserByToken(token);
            }
            
            return null;
        }

        /// <inheritdoc />
        public async Task<UserDto[]> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            return _mapper.Map<UserDto[]>(users);
        }

        /// <inheritdoc />
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));
            
            var user = await _userRepository.GetByIdAsync(id);
            var token = await _userRepository.GetUserTokens(user.Id);
            user.RefreshTokens = token;
            var roles = await _userRepository.GetUserRolesAsync(user.Id);
            user.Roles = roles;
            
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc />
        public async Task<bool> RemoveUserAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));

            return await _userRepository.RemoveUserAsync(id);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateUserInfoAsync(UpdaterUserRequest request)
        {
            Guard.NotNull(request, nameof(request));
            
            return await _userRepository.UpdateUserInfoAsync(request.UserId, request.Login);
        }
    }
}