using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Entities;
using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationSettingsConfig _appSettings;
        private readonly IMapper _mapper;

        public AccountService(
            IOptions<AuthenticationSettingsConfig> appSettings, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc />
        public async Task<AuthenticateInfo> AuthenticateAsync(AuthenticateRequest request, string ipAddress)
        {
            Guard.NotNull(request, nameof(request));
            
            var user = await _userRepository.GetByLoginAsync(request.Username);
            var roles = await _userRepository.GetUserRolesAsync(user.Id);
            var hasher = new PasswordHasher<RegisterUserRequest>();

            if (hasher.VerifyHashedPassword(_mapper.Map<RegisterUserRequest>(user), user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new  ValidationException( "Логин или пароль не верны");
            }
            
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Token = jwtToken;
            
            await _userRepository.CreateUserToken(refreshToken, user.Id);
            
            return new AuthenticateInfo
            {
                Login = user.Login,
                Id = user.Id,
                JwtToken = jwtToken,
                Roles = roles,
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
            var jwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            refreshToken.Token = jwtToken;
            await _userRepository.UpdateUserToken(newRefreshToken);
            


            return new AuthenticateInfo
            {
                Login = user.Login,
                Id = user.Id,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
            };
        }
        
        /// <inheritdoc />
        public async Task<bool> RegisterUserAsync(RegisterUserRequest request, string ip)
        {
            Guard.NotNull(request, nameof(request));
            
            var hasher = new PasswordHasher<RegisterUserRequest>();
            var passwordHash = hasher.HashPassword(request, request.Password);

            var refreshToken = GenerateRefreshToken(ip);

            var user = await _userRepository.GetByLoginAsync(request.Login);

            if (user is not null)
            {
                throw new ValidationException("Логин или пароль не верны");
            }

            return  await _userRepository.CreateUserAsync(request.Login, passwordHash, refreshToken);
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
            using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
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
    }
}