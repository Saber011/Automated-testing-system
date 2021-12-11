using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Postgres.Entities;
using Automated.Testing.System.DataAccess.Postgres.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationSettingsConfig _appSettings;

        public UserService(
            IOptions<AuthenticationSettingsConfig> appSettings, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc />
        public async Task<UserDto[]> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            return MapToDto(users);
        }

        /// <inheritdoc />
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));
            
            var user = await _userRepository.GetByIdAsync(id);
            var token = await _userRepository.GetUserTokens(user.Id);
            user.RefreshTokens = token;
            
            return MapToDto(user);
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
            
            return await _userRepository.UpdateUserInfoAsync(request.UserId, request.Login, request.Password);
        }

        private static UserDto[] MapToDto(User[] users)
        {
            return users.Select(MapToDto)
                .ToArray();
        }
        
        private static UserDto MapToDto(User user)
        {
            return new()
            {
                Id = user.Id,
                Login = user.Login,
                RefreshTokens = user.RefreshTokens
            };
        }
        
        private RegisterUserRequest MapToRequest(User user)
        {
            return new()
            {
                Password = user.Password,
                Login = user.Login,
            };
        }
    }
}