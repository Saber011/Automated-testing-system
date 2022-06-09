namespace Automated.Testing.System.Analytics.Entities.Models
{
    /// <summary>
    /// Информация о активном пользователе
    /// </summary>
    public sealed class ActiveUserInfo
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        
        /// <summary>
        /// Количество выполненых задач
        /// </summary>
        public int NumberCompletedTasks { get; set; }
    }
}