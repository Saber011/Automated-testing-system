using System;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetMostActiveUsers
{
    /// <summary>
    /// Запрос на получение самых активных пользователей
    /// топ 3
    /// </summary>
    public class GetMostActiveUsersRequest : IRequest<ActiveUserInfoDto[]>
    {

    }
}
