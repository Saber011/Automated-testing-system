using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetExecuteTaskInfo
{
    /// <summary>
    /// Запрос на статистику по пройденным тестам в каждой категории
    /// </summary>
    public class GetExecuteTaskInfoRequest : IRequest<ExecuteTaskInfoDto[]>
    {

    }
}
