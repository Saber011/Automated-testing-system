using System.Text.Json.Serialization;

namespace Automated.Testing.System.Common.User.Dto
{
    /// <summary>
    /// Ответ об авторизации
    /// </summary>
    public class AuthenticateInfo
    {
        /// <summary>
        /// id Пользователя.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Роли
        /// </summary>
        public int[] Roles { get; set; }
        
        /// <summary>
        /// Токен
        /// </summary>
        public string JwtToken { get; set; }


        public string RefreshToken { get; set; }
    }
}