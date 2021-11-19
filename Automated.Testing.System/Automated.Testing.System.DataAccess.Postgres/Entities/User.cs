﻿using System;
using System.Collections.Generic;
using Automated.Testing.System.Core.Core;

namespace Automated.Testing.System.DataAccess.Postgres.Entities
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
        /// Рефреш токены.
        /// </summary>
        public RefreshToken[] RefreshTokens { get; set; }
    }
}