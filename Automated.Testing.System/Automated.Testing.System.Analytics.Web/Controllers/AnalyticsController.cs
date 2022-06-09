using System;
using System.Threading.Tasks;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetExecuteTaskInfo;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetMostActiveUsers;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetStatistic;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetUserExecuteTestInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Api для работы с аналитикой
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[Produces("application/json")]
public class AnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnalyticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить статистику по тестам.
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    public async Task<TestStatisticDto[]> GetTestStatistic(DateTime startDate, DateTime endDate, bool isDelete)
    {
        return await _mediator.Send(new GetTestStatisticRequest { StartDate = startDate, EndDate = endDate, IsDeleteStatistic = isDelete});
    }
    
    /// <summary>
    /// Получить статистику по пройденным тестам в каждой категории.
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    public async Task<ExecuteTaskInfoDto[]> GetExecuteTaskInfo()
    {
        return await _mediator.Send(new GetExecuteTaskInfoRequest());
    }
    
    /// <summary>
    /// Получить самых активных пользователей топ 3.
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    public async Task<ActiveUserInfoDto[]> GetMostActiveUsers()
    {
        return await _mediator.Send(new GetMostActiveUsersRequest());
    }
    
    /// <summary>
    /// Получить активность пользователя.
    /// </summary>
    /// <response code = "200" > Успешное выполнение.</response>
    /// <response code = "500" > Непредвиденная ошибка сервера.</response>
    [HttpGet]
    public async Task<UserExecuteTestInfoDto[]> GetUserExecuteTestInfo(int userId)
    {
        return await _mediator.Send(new GetUserExecuteTestInfoRequest { UserId = userId});
    }
}