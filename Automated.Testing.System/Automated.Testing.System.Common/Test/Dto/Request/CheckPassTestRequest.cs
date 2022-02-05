using System.ComponentModel.DataAnnotations;

namespace Automated.Testing.System.Common.Test.Dto.Request
{
    /// <summary>
    /// Проверить результат прохождения теста
    /// </summary>
    public sealed class CheckPassTestRequest
    {
        /// <summary>
        /// Id теста
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Id теста
        /// </summary>
        [Required]
        public ExecuteTask[] ExecuteTasks { get; set; }
    }
}