using System.Collections.Generic;
using Automated.Testing.System.Core.Core;

namespace Automated.Testing.System.Common.User.Dto
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public sealed class UserDto
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
        /// Роли
        /// </summary>
        public int[] Roles { get; set; }
        
        /// <summary>
        /// Рефреш токены.
        /// </summary>
        public RefreshToken[] RefreshTokens { get; set; }
    }
}