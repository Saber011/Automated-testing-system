using Automated.Testing.System.UseCases.Handlers.Dictionary.Dto;

namespace Automated.Testing.System.UseCases.Handlers.Dictionary.Mappings
{
    public class DictionaryAutoMapperProfile : AutoMapper.Profile
    {
        public DictionaryAutoMapperProfile()
        {
            CreateMap<DataAccess.Abstractions.Entities.Dictionary, DictionaryDto>();
        }
    }
}
