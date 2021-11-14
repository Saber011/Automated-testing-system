namespace Automated.Testing.System.Common.User.Dto.Request
{
    public class RegisterUserRequest
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}