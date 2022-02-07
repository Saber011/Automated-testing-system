using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.Analytics.Infrastructure.Interfaces.DataAccess.Repositories;
using Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Dto;
using MediatR;

namespace Automated.Testing.System.Analytics.UseCases.Handlers.Statistic.Queries.GetExecuteTaskInfo
{
    internal class GetExecuteTaskInfoRequestHandler : IRequestHandler<GetExecuteTaskInfoRequest, ExecuteTaskInfoDto[]>
    {
        private readonly IStatisticRepository  _statisticRepository;
        private readonly IMapper _mapper;

        public GetExecuteTaskInfoRequestHandler(IStatisticRepository repository, IMapper mapper)
        {
            _statisticRepository = repository;
            _mapper = mapper;
        }

        public async Task<ExecuteTaskInfoDto[]> Handle(GetExecuteTaskInfoRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ExecuteTaskInfoDto[]>(await _statisticRepository.GetCompletedTestsOnCategory());
        }
    }
}
