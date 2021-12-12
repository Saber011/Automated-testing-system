using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.DataAccess.Interfaces;
using MediatR;
using DictionaryDto = Automated.Testing.System.UseCases.Handlers.Dictionary.Dto.DictionaryDto;

namespace Automated.Testing.System.UseCases.Handlers.Dictionary.Queries.GetAllDictionary
{
    internal class GetAllDictionaryRequestHandler : IRequestHandler<GetAllDictionaryRequest, DictionaryDto[]>
    {
        private readonly IDictionaryRepository _dbContext;
        private readonly IMapper _mapper;
        private readonly IDictionaryService _dictionaryService;

        public GetAllDictionaryRequestHandler(IDictionaryRepository dbContext, IMapper mapper, IDictionaryService dictionaryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dictionaryService = dictionaryService;
        }

        public async Task<DictionaryDto[]> Handle(GetAllDictionaryRequest request, CancellationToken cancellationToken)
        {
            var dictionaries = await _dbContext.GetAllDictionaryAsync();

            await _dictionaryService.GetAllAsync();
            
            return _mapper.Map<DictionaryDto[]>(dictionaries);;
        }
    }
}
