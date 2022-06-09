using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetUserExecuteTestInfo
{
    /// <summary>
    /// Запрос на активность пользователя
    /// </summary>
    public class GetUserExecuteTestInfoRequest : IRequest<UserExecuteTestInfoDto[]>
    {
        /// <summary>
        /// id Пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
