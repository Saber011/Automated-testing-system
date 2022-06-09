using System;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetStatistic
{
    /// <summary>
    /// Запрос на получение статистики по тестам
    /// </summary>
    public class GetTestStatisticRequest : IRequest<TestStatisticDto[]>
    {
        /// <summary>
        /// Дата начала выбора
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Дата окончания выбора
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Выборка по удаленным тестам
        /// </summary>
        public bool IsDeleteStatistic { get; set; }
    }
}
