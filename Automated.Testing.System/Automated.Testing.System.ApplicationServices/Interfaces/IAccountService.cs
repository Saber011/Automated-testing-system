using System.Threading.Tasks;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;

namespace Automated.Testing.System.ApplicationServices.Interfaces
{
    /// <summary>
    /// Сервис для работы с авторизацией
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получить аунтификацию.
        /// </summary>
        Task<AuthenticateInfo> AuthenticateAsync(AuthenticateRequest request, string ipAddress);
        
        /// <summary>
        /// Получить рефреш токен.
        /// </summary>
        Task<AuthenticateInfo> RefreshTokenAsync(string token, string ipAddress);
        
        /// <summary>
        /// Удалить токен
        /// </summary>
        Task<bool> RevokeTokenAsync(string token, string ipAddress);
        
        /// <summary>
        /// Добавить пользователя.
        /// </summary>
        Task<bool> RegisterUserAsync(RegisterUserRequest request, string ipAddress);
    }
}