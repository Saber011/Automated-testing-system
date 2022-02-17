using AutoMapper;
using Automated.Testing.System.Analytics.Entities.Models;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Mappings
{
    public class StatisticAutoMapperProfile : Profile
    {
        public StatisticAutoMapperProfile()
        {
            CreateMap<TestStatistic, TestStatisticDto>();
            CreateMap<ActiveUserInfo, ActiveUserInfoDto>();
            CreateMap<ExecuteTaskInfo, ExecuteTaskInfoDto>();
            CreateMap<UserExecuteTestInfo, UserExecuteTestInfoDto>();
        }
    }
}
