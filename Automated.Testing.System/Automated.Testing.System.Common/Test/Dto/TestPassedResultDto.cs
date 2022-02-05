using System.ComponentModel.DataAnnotations;

namespace Automated.Testing.System.Common.Test.Dto
{
    /// <summary>
    /// Результат прохождения теста.
    /// </summary>
    public sealed class TestPassedResultDto
    {
        /// <summary>
        /// Количество правильных ответов
        /// </summary>
        [Required]
        public int CountCorrectAnswer { get; set; }
        
        /// <summary>
        /// Количество задач в тесте
        /// </summary>
        [Required]
        public int CountTask { get; set; }
    }
}