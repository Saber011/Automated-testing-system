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
        public async Task<AuthenticateInfo> AuthenticateAsync(AuthenticateRequest request, string ipAddress)
        {
            Guard.NotNull(request, nameof(request));
            
            var user = await _userRepository.GetByLoginAsync(request.Username);
            var hasher = new PasswordHasher<RegisterUserRequest>();

            if (user == null || hasher.VerifyHashedPassword(MapToRequest(user), user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new  ValidationException( "Username or password is incorrect");
            }
            
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(ipAddress);
            
            await _userRepository.CreateUserToken(refreshToken, user.Id);
            
            return new AuthenticateInfo
            {
                Login = user.Login,
                Id = user.Id,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
            };
        }

        /// <inheritdoc />
        public async Task<AuthenticateInfo> RefreshTokenAsync(string token, string ipAddress)
        {
            Guard.NotNullOrWhiteSpace(token, nameof(token));
            
            var usersId = await _userRepository.GetUseridByTokens(token);
            
            if (usersId == null)
                return null;
            var user = await _userRepository.GetByIdAsync(usersId!.Value);
            var userToken = await _userRepository.GetUserTokens(usersId!.Value);
            

            var refreshToken = userToken.Single(x => x.Token == token);
            
            if (!refreshToken.IsActive)
                return null;
            
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            await _userRepository.UpdateUserToken(newRefreshToken);
            
            var jwtToken = GenerateJwtToken(user);

            return new AuthenticateInfo
            {
                Login = user.Login,
                Id = user.Id,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
            };
        }

        /// <inheritdoc />
        public async Task<bool> RevokeTokenAsync(string token, string ipAddress)
        {
            Guard.NotNullOrWhiteSpace(token, nameof(token));
            
            var usersId = await _userRepository.GetUseridByTokens(token);
            if (usersId == null)
                return false;
            var tokens = await _userRepository.GetUserTokens(usersId.Value);

            var refreshToken = tokens.Single(x => x.Token == token);
            
            if (!refreshToken.IsActive)
                return false;
            
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _userRepository.UpdateUserToken(refreshToken);

            return true;
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
        public async Task<bool> CreateUserAsync(RegisterUserRequest request, string ip)
        {
            Guard.NotNull(request, nameof(request));
            
            var hasher = new PasswordHasher<RegisterUserRequest>();
            var passwordHash = hasher.HashPassword(request, request.Password);

            var refreshToken = GenerateRefreshToken(ip);

            var loginNotExist = await _userRepository.GetByLoginAsync(request.Login) is not null;

            if (!loginNotExist)
            {
                throw new  ValidationException( "Username or password is incorrect");
            }

            return  await _userRepository.CreateUserAsync(request.Login, passwordHash, refreshToken);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateUserInfoAsync(UpdaterUserRequest request)
        {
            Guard.NotNull(request, nameof(request));
            
            return await _userRepository.UpdateUserInfoAsync(request.UserId, request.Login, request.Password);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
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