using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetStatistic;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetMostActiveUsers
{
    internal class GetMostActiveUsersRequestHandler : IRequestHandler<GetMostActiveUsersRequest, ActiveUserInfoDto[]>
    {
        private readonly IStatisticRepository  _statisticRepository;
        private readonly IMapper _mapper;

        public GetMostActiveUsersRequestHandler(IStatisticRepository repository, IMapper mapper)
        {
            _statisticRepository = repository;
            _mapper = mapper;
        }

        public async Task<ActiveUserInfoDto[]> Handle(GetMostActiveUsersRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ActiveUserInfoDto[]>(await _statisticRepository.GetMostActivityUser());
        }
    }
}
