using System.ComponentModel.DataAnnotations;

namespace Automated.Testing.System.Common.Test
{
    /// <summary>
    /// Выполяемая задача
    /// </summary>
    public sealed class ExecuteTask
    {
        /// <summary>
        /// id задачи
        /// </summary>
        public int TaskId { get; set; }
        
        /// <summary>
        /// Ответ
        /// </summary>
        public string? Answer { get; set; }
    }
}