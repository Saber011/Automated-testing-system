using System.Threading.Tasks;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Entities;

namespace Automated.Testing.System.DataAccess.Abstractions.Interfaces
{
    /// <summary>
    /// Репозиторий для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <returns></returns>
        Task<User[]> GetAllAsync();
        
        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Получить Роли пользователя.
        /// </summary>
        Task<int[]> GetUserRolesAsync(int userId);

        /// <summary>
        /// Получить пользователя по логину.
        /// </summary>
        Task<User> GetByLoginAsync(string login);
        
        /// <summary>
        /// Получить пользователя по токену.
        /// </summary>
        Task<User?> GetUserByToken(string token);
        
        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<bool> CreateUserAsync(string login, string password, RefreshToken refreshTokens);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<bool> RemoveUserAsync(int id);
        
        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        Task<bool> UpdateUserInfoAsync(int id, string login);

        /// <summary>
        /// Получить токены пользователя.
        /// </summary>
        Task<RefreshToken[]> GetUserTokens(int id);
        
        /// <summary>
        /// Получить токены пользователя.
        /// </summary>
        Task<int?> GetUseridByTokens(string token);
        
        /// <summary>
        /// Получить токены пользователя.
        /// </summary>
        Task<bool> UpdateUserToken(RefreshToken token);
        
        /// <summary>
        /// Добавить новый токен пользователя.
        /// </summary>
        Task<bool> CreateUserToken(RefreshToken token, int id);
    }
}