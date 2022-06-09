using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetStatistic;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetUserExecuteTestInfo
{
    internal class GetUserExecuteTestInfoRequestHandler : IRequestHandler<GetUserExecuteTestInfoRequest, UserExecuteTestInfoDto[]>
    {
        private readonly IStatisticRepository  _statisticRepository;
        private readonly IMapper _mapper;

        public GetUserExecuteTestInfoRequestHandler(IStatisticRepository repository, IMapper mapper)
        {
            _statisticRepository = repository;
            _mapper = mapper;
        }

        public async Task<UserExecuteTestInfoDto[]> Handle(GetUserExecuteTestInfoRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserExecuteTestInfoDto[]>(await _statisticRepository.GetUserActivity(request.UserId));
        }
    }
}
