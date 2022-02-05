using Automated.Testing.System.Core.Core;

namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// id.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Роли
        /// </summary>
        public int[] Roles { get; set; }
        
        /// <summary>
        /// Рефреш токены.
        /// </summary>
        public RefreshToken[] RefreshTokens { get; set; }
    }
}