using System;

namespace Automated.Testing.System.Analytics.Entities.Models
{
    /// <summary>
    /// Статистика по количествую созданных тестов
    /// </summary>
    public sealed class TestStatistic
    {
        /// <summary>
        /// Год создания
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// Месяц создания
        /// </summary>
        public int Mount { get; set; }
        
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
    }
}
