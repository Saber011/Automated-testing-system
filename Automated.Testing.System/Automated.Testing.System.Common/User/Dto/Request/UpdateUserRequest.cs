namespace Automated.Testing.System.Common.User.Dto.Request
{
    public class UpdaterUserRequest
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int UserId { get; set; }
        
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