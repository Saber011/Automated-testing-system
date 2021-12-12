using MediatR;
using DictionaryDto = Automated.Testing.System.UseCases.Handlers.Dictionary.Dto.DictionaryDto;

namespace Automated.Testing.System.UseCases.Handlers.Dictionary.Queries.GetAllDictionary
{
    public class GetAllDictionaryRequest : IRequest<DictionaryDto[]>
    {
    }
}
