using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetStatistic;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetTestStatistic
{
    internal class GetTestStatisticRequestHandler : IRequestHandler<GetTestStatisticRequest, TestStatisticDto[]>
    {
        private readonly IStatisticRepository  _statisticRepository;
        private readonly IMapper _mapper;

        public GetTestStatisticRequestHandler(IStatisticRepository repository, IMapper mapper)
        {
            _statisticRepository = repository;
            _mapper = mapper;
        }

        public async Task<TestStatisticDto[]> Handle(GetTestStatisticRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TestStatisticDto[]>(await _statisticRepository.GetTestStatistic(request.StartDate, request.EndDate, request.IsDeleteStatistic));
        }
    }
}
