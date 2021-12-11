using System.Threading.Tasks;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;

namespace Automated.Testing.System.ApplicationServices.Interfaces
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <returns></returns>
        Task<UserDto[]> GetAllUsersAsync();
        
        /// <summary>
        /// Получить пользователя по id.
        /// </summary>
        Task<UserDto> GetUserByIdAsync(int id);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        Task<bool> RemoveUserAsync(int id);

        /// <summary>
        /// Изменить данные пользователя.
        /// </summary>
        Task<bool> UpdateUserInfoAsync(UpdaterUserRequest request);
    }
}